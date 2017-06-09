$(document).ready(function () {
    BindOpener();
    BindSender();
});

function BindOpener() {
    $('#back-chooseTemplate').click(function () {

        $('#dialog').dialog('open');
        $('#dialogChoose').dialog('close');
        $("#dialogChangeArticle").dialog('close');
        $('#dialogPreview').dialog('close');
    });
}

function BindSender() {
    $('#next-chooseTemplate2').click(function () {

        var fileUpload = $("#file").get(0);

        var files = fileUpload.files;

        for (var i = 0; i < files.length ; i++) {
            data.append(files[i].name, files[i]);
        }

        var postData = $('#title');
        $.each(postData, function (key, input) {
            data.append(input.name, input.value);
        });

        postData = $('#description');
        $.each(postData, function (key, input) {
            data.append(input.name, input.value);
        });
        data.append("template", "1");
        data.append("Chapter", $("#chapterId :selected").text());

        $.ajax({
            type: "POST",
            url: '/Admin/GetTemplatePartial',
            contentType: false,
            processData: false,
            data: data,
            success: function (data) {
                if (data.indexOf("<form method=") != -1) {
                    $('#dialogPreview').html(data);
                }
                else {
                    $('#dialogChoose').dialog('open');
                    $('#dialogChoose').html(data);
                }
            },
            error: function () {
                alert("There was error uploading files!");
            }
        });

    });
}

