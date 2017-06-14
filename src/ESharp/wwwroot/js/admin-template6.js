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

String.prototype.replaceAll = function (search, replace) {
    return this.split(search).join(replace);
}

function BindSender() {
    $('#next-chooseTemplate2').click(function () {


        var postData = $('#title');
        $.each(postData, function (key, input) {
            data.append(input.name, input.value);
        });

        postData = $('#description');
        $.each(postData, function (key, input) {
            data.append(input.name, input.value);
        });


        data.append("template", "6");
        data.append("Chapter", $("#chapterId :selected").text());
        data.append("OldChapter", $("#oldchapter").val());
        data.append("OldArticle", $("#oldarticle").val());

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
                    data = data.replaceAll("&#xD;&#xA;", "<br>");
                    data = data.replaceAll("&lt;b&gt;", "<b>");
                    data = data.replaceAll("&lt;/b&gt;", "</b>");
                    data = data.replaceAll("&lt;i&gt;", "<i>");
                    data = data.replaceAll("&lt;/i&gt;", "</i>");
                    data = data.replaceAll("&#xD;", "<br>");
                    data = data.replaceAll("&#xA;", "<br>");
                    $('#dialogChoose').html(data);
                }
            },
            error: function () {
                alert("There was error uploading files!");
            }
        });

    });
}

