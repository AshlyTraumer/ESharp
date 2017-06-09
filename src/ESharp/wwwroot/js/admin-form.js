$(document).ready(function () {
    BindNewArticleButtonClick();
    BindNewChapterButtonClick();
    BindGetFormButtonClick();
    ChangeArticleView();
});

function BindNewArticleButtonClick() {
    $('#newArticle').click(function () {
        $('#dialog').dialog('open');
    });
}

function BindNewChapterButtonClick() {
    $('#newChapter').click(function () {
        $('#dialogChapter').dialog('open');

        $.ajax({
            type: "Get",
            url: '/Admin/GetChapterPartial',
            dataType: 'html',
            success: function (data) {
                $('#dialogChapter').html(data);
            }
        });
    });
}

function BindGetFormButtonClick() {
    $('.template').click(function () {
        $('#dialog').dialog('close');
        $('#dialogChoose').dialog('close');
        $("#dialogChangeArticle").dialog('close');
        $('#dialogPreview').dialog('open');

        var data = new FormData();
        data.append("Get", "");
        data.append("template", $(this).data().template);
        $.ajax({
            type: "POST",
            url: "/Admin/GetTemplatePartial",
            contentType: false,
            processData: false,
            data: data,
            success: function (data) {
                $('#dialogPreview').html(data);
            }
        });

        $(document).ajaxStart(function () {
            $('#dialogChoose').html("Загрузка");
        });

        $('#next-chooseOk').click(function () {

            $('#dialogOk').dialog('close');
        });
    });
}

function ChangeArticleView() {
    $('.change-article-btn').click(function () {
            var chapter = $(this).data().chapter;
            var article = $(this).data().article;
            var isA = confirm("Внимание. При изменении статьи старые картинки не сохраняются. Вы желаете продолжить?");
            if (isA == false) return;

            $("#dialogPreview").dialog('open');

            $.ajax({
                type: "GET",
                url: '/Admin/ChangeArticleView' + '?article=' + article + '&chapter=' + chapter,
                contentType: false,
                processData: false,

                success: function(data) {
                    $('#dialogPreview').html(data);
                }
            });
        });
}