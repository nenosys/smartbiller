var billing = function() {
    return {

        visitCostsState : {
            selectedRowId: null,
            selectedColumn:null
        },
        selectedPatientId: null,
        selectedVisitId:null,
        savePatient:function(id) {
            var data = {
                identifier: $("#patientdetails-identifier").val(),
                firstName: $("#patientdetails-first-name").val(),
                lastName: $("#patientdetails-last-name").val(),
                age: $("#patientdetails-age").val(),
                patientId: id
            };
            common.post("/patients/SavePatientDetails", data, function (response) {
                if (!response.success) common.showMessage(response.message);
                else {
                    common.closeDialog();
                    window.location = "/";
                }
            });
        },
        deletePatient : function() {
            common.post("/patients/deletepatient", {id:billing.selectedPatientId}, function (response) {
                if (!response.success) alert(response.message);
                else {
                    window.location = "/";
                }
            });
        },
        saveVisit:function(visitId) {
            var data = {
                inDate: $("#visit-in-date").val(),
                outDate: $("#visit-out-date").val(),
                discharged: $("#visit-discharged").is(":checked"),
                patientId: billing.selectedPatientId,
                visitId: visitId
            };
            common.post("/patients/savevisit", data, function (response) {
                if (!response.success) common.showMessage(response.message);
                else {
                    common.closeDialog();
                    window.location = "/patients/visits?patientId=" + billing.selectedPatientId;
                }
            });
        },
        deleteVisit: function (visitId) {
            common.post("/patients/deletevisit", { id: visitId }, function (response) {
                if (!response.success) alert(response.message);
                else {
                    window.location = "/patients/visits?patientId=" + billing.selectedPatientId;
                }
            });
        },
        autoComplete: function (input) {
            $(input).typeahead({
                source:
                    function (query, process) {
                        $.get("/paitents/patients/GetCategories?q=" + query, function (data) {
                            process(data);
                        });
                    },
                autoSelect: false,
                afterSelect: function (selectedItem) {
                    //window.location = "/products/home/product?id=" + selectedItem.id;
                }
            });
        },
        updateVisitCostRow: function (categoryId,visitCostId, quantity, totalUnits, startDate, endDate) {
            var data = {
                categoryId,
                visitId : billing.selectedVisitId,
                visitCostId,
                quantity,
                totalUnits,
                startDate,
                endDate
            };
            $.post("/patients/UpdateVisitCostRow", data, function (response) {

                if (visitCostId == "0") {
                    $("#patient-visit-costs tr[data-id='0']").attr("data-id", response.id);
                    $("#patient-visit-costs").find('tbody')
                        .append($('<tr data-id="0">' +
                            '<td class="category"><input class="row-edit"type="text" value="" />' +
                            '<td class="quantity"><input class="row-edit cost-quantity" type="text" value="1"/></td>' +
                            '<td class="rate"></td>' +
                            '<td class="units"><input class="row-edit chargable-units"  type="text" value="1" /></td>' +
                            '<td class="cost"></td>' +
                            '<td><button type="button" class="btn btn-danger delete-cost" data-action="delete" >Delete</button></td>'));
                    $("#patient-visit-costs").scrollTop($("#patient-visit-costs")[0].scrollHeight);
                } else {
                    
                }

                $("#patient-visit-costs tr[data-id='" + response.id + "'] td.rate").html(response.rate);
                $("#patient-visit-costs tr[data-id='" + response.id + "'] td.cost").html(response.totalCost);
                $("#total-payable").html(response.totalPayable);
                billing.bindVisitCostsEvents();
               
            });
        },
        saveCategory: function (categoryId) {
            var data = {
                name: $("#category-name").val(),
                departmentId: $("#category-department").val(),
                rateTypeId: $("#category-rate-types").val(),
                chargePerUnit: $("#category-rate-per-unit").val(),
                threshold: $("#category-threshold").val(),
                chargeAfterUpperBound: $("#category-rate-per-unit-after-threshold").val(),
                categoryId: categoryId
            };
            common.post("/patients/savecategory", data, function (response) {
                if (!response.success) common.showMessage(response.message);
                else {
                    common.closeDialog();
                    window.location = "/patients/billingcategories";
                }
            });
        },
        deleteCategory: function (id) {
            common.post("/patients/deletecategory", { id: id }, function (response) {
                if (!response.success) alert(response.message);
                else {
                    window.location = "/patients/billingcategories";
                }
            });
        },
        deleteCost: function (id) {
            common.post("/patients/deletecost", { costId: id, visitId : billing.selectedVisitId }, function (response) {
                $("#visit-costs").html(response);
            });
        },
        bindVisitCostsEvents: function () {

            $(".row-edit.chargable-units").unbind("focusout");
            $(".row-edit.cost-quantity").unbind("focusout");
            $(".delete-cost").unbind("click");
          
            $(".row-edit.chargable-units").focusout(function () {
                var text = $(this).val();
                var id = $(this).parent().parent().attr("data-id");
                var categoryId = $(this).parent().parent().attr("data-categoryid");
                billing.updateVisitCostRow(categoryId, id, null, text, null, null);
            });

            $(".row-edit.cost-quantity").focusout(function () {
                var text = $(this).val();
                var id = $(this).parent().parent().attr("data-id");
                var categoryId = $(this).parent().parent().attr("data-categoryid");
                billing.updateVisitCostRow(categoryId, id, text, null, null, null);
            });

            $(".category .row-edit").each(function () {
                var me = $(this);
                $(me).typeahead({
                    source:
                        function (query, process) {
                            $.get("/patients/categories?q=" + query, function (data) {
                                process(data);
                            });
                        },
                    autoSelect: false,
                    afterSelect: function (selectedItem) {
                        $(me).parent().parent().attr("data-categoryid", selectedItem.id);
                        var visitCostId = $(me).parent().parent().attr("data-id");
                        billing.updateVisitCostRow(selectedItem.id, visitCostId, null, null, null, null);
                        //window.location = "/products/home/product?id=" + selectedItem.id;
                    }
                });

            });

            $(".delete-cost").click(function () {
                var costId = $(this).parent().parent().attr("data-id");
                common.confirm("Delete cost", "Are you sure you want to delete the cost?", function () {
                    billing.deleteCost(costId);
                    common.closeDialog();
                });
            });

        }
    };
}();