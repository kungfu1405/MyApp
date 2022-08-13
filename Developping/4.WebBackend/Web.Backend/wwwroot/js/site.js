toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": false,
    "progressBar": false,
    "positionClass": "toast-top-right",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "7000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};

function alertMessages(msgs) {
    if (msgs === undefined)
        return;

    for (var i = 0; i < msgs.length; i++) {
        alertMessage(msgs[i]);
    }
}
function alertMessage(msg) {
    switch (msg.status) {
        case 0:
            toastr.success(msg.message);
            break;
        case 1:
            toastr.info(msg.message);
            break;
        case 2:
            toastr.warning(msg.message);
            break;
        case 3:
            toastr.error(msg.message);
            break;
        default:
            toastr.info(msg.message);
            break;
    }
}

function initFormOptions() {
    $('input[maxlength]:not([readonly]), textarea[maxlength]:not([readonly])').maxlength({
        threshold: 0,
        warningClass: "label label-warning label-rounded label-inline",
        limitReachedClass: "label label-danger label-rounded label-inline"
    });
}

function newUUID() {
    var dt = new Date().getTime();
    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (dt + Math.random() * 16) % 16 | 0;
        dt = Math.floor(dt / 16);
        return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
    return uuid;
}

function download(uri, filename) {
    var element = document.createElement('a');
    element.setAttribute('href', uri);
    element.setAttribute('download', filename);
    element.style.display = 'none';

    document.body.appendChild(element);
    element.click();
    document.body.removeChild(element);
}

function addOverlay(obj, timeout) {
    $(obj).addClass("overlay overlay-block");
    $(obj).append('<div class="overlay-layer bg-success-o-20" style="z-index:999999999"><div class="spinner spinner-lg spinner-primary"></div></div>');

    if (timeout && parseInt(timeout) > 0) {
        setTimeout(function () {
            removeOverlay(obj);
        }, timeout)
    }
}
function removeOverlay(obj) {
    $(obj).removeClass("overlay overlay-block");
    $(obj).find('.overlay-layer').remove();
}

function loadQuickView(obj) {
    $('#quickview-title').html('');
    $('#quickview-subtitle').html('');
    $('#quickview-body').html('');

    var durl = $(obj).attr('data-link');
    if (durl == undefined) return;

    var dtitle = $(obj).attr('data-title');
    if (dtitle != undefined)
        $('#quickview-title').html(dtitle);

    var dsubtitle = $(obj).attr('data-subtitle');
    if (dsubtitle != undefined)
        $('#quickview-subtitle').html(dsubtitle);

    KTUtil.getById('btn-quickview-toggle').click();
    addOverlay('#quickview-body', 3000);

    var options = new Object();
    options.url = durl;
    options.type = 'GET';
    options.success = function (data) {
        removeOverlay('#quickview-body');
        if (data) {
            $('#quickview-body').html(data);
        }

    };
    options.error = function (xhr) { // if error occured
        removeOverlay('#quickview-body');
    };
    $.ajax(options);
}

var KTLayoutQuickView = function (id) {
    var _element = KTUtil.getById(id);
    var header = KTUtil.find(_element, '.offcanvas-header');
    var content = KTUtil.find(_element, '.offcanvas-content');

    _offcanvasObject = new KTOffcanvas(_element, {
        overlay: true,
        baseClass: 'offcanvas',
        placement: 'right',
        closeBy: 'pnlQuickView_close',
        toggleBy: 'btn-quickview-toggle'
    });

    KTUtil.scrollInit(content, {
        disableForMobile: true,
        resetHeightOnDestroy: true,
        handleWindowResize: true,
        height: function () {
            var height = parseInt(KTUtil.getViewPort().height);

            if (header) {
                height = height - parseInt(KTUtil.actualHeight(header));
                height = height - parseInt(KTUtil.css(header, 'marginTop'));
                height = height - parseInt(KTUtil.css(header, 'marginBottom'));
            }

            if (content) {
                height = height - parseInt(KTUtil.css(content, 'marginTop'));
                height = height - parseInt(KTUtil.css(content, 'marginBottom'));
            }

            height = height - parseInt(KTUtil.css(_element, 'paddingTop'));
            height = height - parseInt(KTUtil.css(_element, 'paddingBottom'));

            height = height - 2;
            return height;
        }
    });
};

function loadQuickEdit(obj) {
    $('#modalUpdateData .modal-title').html('');
    $('#modalUpdateData .modal-body > div[data-scroll]').css({ 'height': 'auto', 'max-height': '500px' });
    $('#pnlUpdateDataForm').html('');
    $('#modalUpdateData .modal-footer .btn-submit').unbind('click');

    var durl = $(obj).attr('data-link');
    if (durl == undefined) return;

    var dtitle = $(obj).attr('data-title');
    if (dtitle != undefined)
        $('#modalUpdateData .modal-title').html(dtitle);

    var dkttable = $(obj).attr('data-kt-table')

    addOverlay('#modalUpdateData .modal-body', 3000);
    $('#modalUpdateData').modal('show');

    var options = new Object();
    options.url = durl;
    options.type = 'GET';
    options.success = function (data) {
        removeOverlay('#modalUpdateData .modal-body');
        if (data) {
            $('#pnlUpdateDataForm').html(data);
            $('#modalUpdateData .modal-body div[data-scroll]').scrollTop(0);

            $('#modalUpdateData .modal-footer .btn-submit').click(function (e) {
                e.preventDefault();
                addOverlay('#modalUpdateData .modal-body', 3000);
                var pform = $('#modalUpdateData .modal-body form');
                console.log('pform', pform);
                var purl = $(pform).attr('action');

                $.ajax({
                    type: "POST",
                    url: purl,
                    data: pform.serialize(), // serializes the form's elements.
                    success: function (data) {
                        removeOverlay('#modalUpdateData .modal-body');
                        $('#pnlUpdateDataForm').html('');
                        $('#modalUpdateData').modal('hide');
                        if (dkttable !== undefined) {
                            $(KTUtil.getById(dkttable)).KTDatatable('reload');
                        }

                        alertMessages(data);
                    },
                    error: function (xhr) { // if error occured
                        removeOverlay('#modalUpdateData .modal-body');
                        toastr.error(xhr.responseText);
                    }
                });

            });
        }
    };
    options.error = function (xhr) { // if error occured
        removeOverlay('#modalUpdateData .modal-body');
    };
    $.ajax(options);
}

function SubmitForm(btn, pnlSelector) {
    if (pnlSelector == undefined || pnlSelector == '' || pnlSelector == null)
        pnlSelector = "body";

    var cmdname = $(btn).attr('data-command');
    $(pnlSelector + " form").each(function () {
        if (cmdname !== undefined) {
            $(this).find('input[name="CommandName"]').val(cmdname);
        }
        $(this).submit();
    });
}

function ConfirmDelete(callback) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            callback();
        }
    });
}

$(document).ready(function () {
    initFormOptions();
    KTLayoutQuickView('pnlQuickView');
    $('[data-toggle="tooltip"]').tooltip();

    $(document).on('click', '.btn-quickview', function (e) {
        e.preventDefault();
        loadQuickView($(this));
    });

    $(document).on('click', '.btn-quickedit', function (e) {
        e.preventDefault();
        loadQuickEdit($(this));
    });

    $(document).on('click', '.input-group .ki.ki-copy', function (e) {
        e.preventDefault();

        var txtid = $(this).parent().parent().parent().find('input.form-control').attr('id');
        var copyText = document.getElementById(txtid);
        copyText.select();
        copyText.setSelectionRange(0, 99999); /*For mobile devices*/
        document.execCommand("copy");
    });
});