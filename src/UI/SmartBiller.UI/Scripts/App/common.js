var common = function () {
    return {
        get: function (url, data, success) {
           
            $.get(url, data, success);
        },
        post: function (url, data, success) {
            if (data == null) data = {};
            $.post(url, data, success);
        },
        init: function (cart) {
           
        },
        getFromLocalStorage : function(key) {
            var value = localStorage.getItem(key);
            return JSON.parse(value);
        },
        setInLocalStorage: function (key, object) {
            localStorage.removeItem(key);
            var value = JSON.stringify(object);
            localStorage.setItem(key,value);
        },
        removeFromLocalStorage: function (key) {
            localStorage.removeItem(key);
        },
      
        openDialog: function (url, title, onSave, onClose) {
            $("#modal-cancel").html("Cancel");
            $("#modal-save-changes").html("Save");
            $(".modal-error-message").html("").hide();
            common.get(url, null, function (response) {
                $(".modal-body").html("");
                $(".modal-title").html(title);
                $("#modal-save-changes").unbind("click");
                if (onSave == null) {
                    $("#modal-save-changes").hide();
                } else {
                    $("#modal-save-changes").click(onSave);
                }
                $("#modal-dialog").modal();
                $(".modal-body").html(response);
            });
        },
        closeDialog : function() {
            $("#modal-dialog").modal('toggle');
        },
        showMessage: function (message) {
            $(".modal-error-message").html(message).show();
        },
        confirm: function (title, message, onYes) {
            $("#modal-cancel").html("No");
            $("#modal-save-changes").html("Yes");
            $(".modal-error-message").html("").hide();
            $(".modal-body").html("");
            $(".modal-title").html(title);
            $("#modal-save-changes").unbind("click");
            if (onYes == null) {
                onYes = function() {
                    common.closeDialog();
                }
            } 
            $("#modal-save-changes").click(onYes);
            $("#modal-dialog").modal();
            $(".modal-body").html(message);
        }
    };
}();