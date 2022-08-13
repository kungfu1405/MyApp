// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
Dropzone.autoDiscover = false;
var dataFiles = [];
$(document).ready(function () {
    //$(".upload-image a").on("click", function () {
    //    $("#modal-upload-image").modal("show");
    //});
    var quill = new Quill('#editor', {
        modules: {
            toolbar: false
        },
        placeholder: 'Add a Description',
        theme: 'snow'
    });


    var id = '#kt_dropzone_2';
    // multiple file upload
    var myDropzone2 = new Dropzone(id, {
        url: "https://192.168.68.121:8113/api/file/upload", // Set the url for your upload script location
        paramName: "file", // The name that will be used to transfer the file
        maxFiles: 10,
        maxFilesize: 10, // MB
        autoQueue: false,
        addRemoveLinks: true,
        thumbnailWidth: 230,
        thumbnailHeight: 230,
        acceptedFiles: ".jpeg,.jpg,.png,.gif",
        uploadprogress: function (file, progress, bytesSent) {
            console.log(file);
        },
        previewsContainer: ".dropzone-box_1"
    });

    myDropzone2.on("addedfiles", function (file) {
        // Hookup the start button
        //file.previewElement.querySelector("form[name=kt_dropzone_2] .dropzone-start").onclick = function () { myDropzone2.enqueueFile(file); };
        //console.log("addedfile");
        var dataFr = new FormData();

        $.each(myDropzone2.files, function (i, file) {
            dataFr.append('file', file);
        });

        if (myDropzone2.files.length == 0) {
            alert("You should be select any file");
        } else if (myDropzone2.getRejectedFiles().length > 0) {
            alert("The attached file is invalid");
        } else {
            //console.log(myDropzone2); return;
            myDropzone2.processQueue();
            $.ajax({
                url: "https://192.168.68.121:8113/api/file/upload",
                type: "POST",
                data: dataFr,
                processData: false,
                contentType: false,
                enctype: "multipart/form-data",
                beforeSend: function () {

                },
                success: function (data, textStatus, xhr) {                    
                    if (data.length > 0) {
                        dataFiles = data;
                    }
                    console.log(dataFiles);
                },
                error: function () {
                    
                }
            });
        }
        
    });

    // Setup the buttons for all transfers
    document.querySelector("#btnUpload").onclick = function (e) {

        e.preventDefault();
        var txtTitle = $("input[name=txtTitle]").val();
        //var des = $("#txtDes").val();
        var des = quill.getText();
        var regex = /(<([^>]+)>)/ig
        hasText = !!des.replace(regex, "").length;

        var errMess = "";

        if (txtTitle.length <= 0) {
            errMess = "Please Input Title!";
        }
        else if (!hasText) {
            errMess = "Please Input Description!";
        }
        else if (myDropzone2.files.length == 0) {
            errMess = "Please Select Any File!";
        }

        if (errMess.length > 0) {
            $(".txtStt_Upload").empty().removeClass("text-success").addClass("text-danger").html(errMess);
            return;
        }

        var data = {
            title: txtTitle,
            description: des,
            files: dataFiles
        };

        $.ajax({
            url: "/Session/Upload",
            type: "POST",
            data: data,
            //dataType: "json",
            beforeSend: function () {
                $(".txtStt_Upload").removeClass("text-danger").removeClass("text-success").empty().html("Uploading ...");
                $("#btnUpload").css("display", "none");
            },
            success: function (data, textStatus, xhr) {
                if (data.status == 1) {
                    $(".txtStt_Upload").empty().removeClass("text-danger").addClass("text-success").html("Upload Success!");
                    myDropzone2.removeAllFiles(true);
                    $("#frmUpload").trigger("reset");
                }
                else {
                    $(".txtStt_Upload").empty().removeClass("text-success").addClass("text-danger").html("Upload Fail!");
                }
            },
            error: function () {
                $(".txtStt_Upload").empty().removeClass("text-success").addClass("text-danger").html("Upload Fail. Please Contact Admin.");
            },
            done: function () {
                $("#btnUpload").css("display", "inline-block");
            }
        });
        
    };

    // Setup the button for remove all files
    document.querySelector("form[name=kt_dropzone_2] .dropzone-remove-all").onclick = function () {
        $("form[name=kt_dropzone_2] .dropzone-upload, form[name=kt_dropzone_2] .dropzone-remove-all").css('display', 'none');
        myDropzone2.removeAllFiles(true);
        $(".dropzone-status-update").empty();
    };


    // On all files removed
    myDropzone2.on("removedfile", function (file) {
        if (myDropzone2.files.length < 1) {
            $("form[name=kt_dropzone_2] .dropzone-upload, form[name=kt_dropzone_2] .dropzone-remove-all").css('display', 'none');
        }
    });
});

function addSession(obj) {
    var count = $(obj).attr("data-sess");
    count++;
    $(".addSession").append($("<span>").html(count));
    $(obj).attr("data-sess", count);

    var cloneSession = $("#first-card").clone().attr("id", "card-" + count).appendTo("#session-upload");
    var id = "kt_dropzone_2_" + count;
    cloneSession.find(".idDropzone").attr("id", id);
    cloneSession.find(".dropzone-box").addClass("kt_dropzone_2_" + id);

    $("#" + id).dropzone({
        url: "https://192.168.68.63:8113/api/file/upload", // Set the url for your upload script location
        paramName: "file", // The name that will be used to transfer the file
        maxFiles: 10,
        maxFilesize: 10, // MB
        autoQueue: false,
        addRemoveLinks: true,
        thumbnailWidth: 230,
        thumbnailHeight: 230,
        uploadprogress: function (file, progress, bytesSent) {
            console.log(file);
        },
        previewsContainer: ".kt_dropzone_2_" + id
    });
}

function uploadExperience() {
    
}