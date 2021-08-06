toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": false,
    "progressBar": false,
    "positionClass": "toast-top-right",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};


function toast(type, message) {
    $(document.body).removeClass('loading');
    switch (type) {
        case "success":
            toastr.success(message, "Success");
            break;
        case "error":
            toastr.error(message, "Error");
            break;
    }
    setTimeout(function () { $('.toast').removeClass('active'); }, 8000);
}

jQuery('body').on('click', '.toast .close', function (e) {
    $(this).parent().removeClass('active');
});

function loader(value) {
    switch (value) {
        case "true":
            $('body').addClass('loading');
            break;
        case "false":
            $('body').removeClass('loading');
            break;
    }
}

function tableLoader(value) {
    switch (value) {
        case "true":
            $('.table-responsive').addClass('component-loading');
            break;
        case "false":
            $('.table-responsive').removeClass('component-loading');
            break;
    }
}

function showNoDataTable(value) {
    switch (value) {
        case "true":
            $('.table-responsive').addClass('no-data-table');
            $('#nodata').show();
            break;
        case "false":
            $('.table-responsive').removeClass('no-data-table');
            $('#nodata').hide();
            break;
    }
}

async function doAjax(url, params, method) {
    try {
        return await $.ajax({
            type: method,
            url: url,
            data: JSON.stringify(params),
            contentType: "application/json",
            success: function (res) {
                var Type = res.type;
                if (Type == "error") {
                    toast("error", res.value);
                }
                else {
                    loader("false")
                    toast("success", res.value);
                }
            }
        });
    } catch (error) {
        console.log(error);
    }
}

async function doAjaxWithReturnUrl(url, params, method) {
    try {
        return await $.ajax({
            type: method,
            url: url,
            data: JSON.stringify(params),
            contentType: "application/json",
            success: function (res) {
                var Type = res.type;
                if (Type == "error") {
                    toast("error", res.value);
                }
                else {
                    window.location.href = res.returnUrl;
                }
            }
        });
    } catch (error) {
        console.log(error);
    }
}

async function doAjaxWithReturnData(url, params, method) {
    try {
        return await $.ajax({
            type: method,
            url: url,
            data: JSON.stringify(params),
            contentType: "application/json",
            traditional: true,
            success: function (res) {
                var Type = res.type;
                if (Type == "error") {
                    toast("error", res.value);
                }
                else {
                    return res.value;
                }
            }
        });
    } catch (error) {
        console.log(error);
    }
}

async function GetMethod(url) {
    try {
        $.ajax({
            type: 'GET',
            url: url,
            cache: false,
            success: function (res) {
                var Type = res.type;
                if (Type == "error") {
                    toast("error", res.value);
                }
                else {
                    toast("success", res.value);
                }
            }
        });
    } catch (error) {

    }
}

async function doForm(url, data, method) {
    try {
        return await $.ajax({
            type: method,
            url: url,
            data: data,
            processData: false,
            contentType: false,
            success: function (res) {
                var Type = res.type;
                if (Type == "error") {
                    toast("error", res.value);
                }
                else {
                    toast("success", res.value);
                }
            }
        });
    } catch (error) {
        console.log(error);
    }
}

async function doFormData(url, data, method) {
    try {
        return await $.ajax({
            type: method,
            url: url,
            data: data,
            processData: false,
            contentType: false,
            success: function (res) {
                var Type = res.type;
                if (Type == "error") {
                    toast("error", res.value);
                }
                else {
                    return res.value;
                }
            }
        });
    } catch (error) {
        console.log(error);
    }
}

function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
};

function addCss() {
    jQuery('.action-col .dropdown-toggle').click(function (e) {
        $(".action-col").removeClass("action-active");
        $(this).parents(".action-col").addClass("action-active");
    });
}
