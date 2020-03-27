
$(function () {
    tinymce.init({
        selector: ".mcetiny",
        theme: "modern",
        plugins: [
         "advlist autolink lists link image charmap print preview hr anchor pagebreak",
         "searchreplace wordcount visualblocks visualchars code fullscreen",
         "insertdatetime media nonbreaking save table contextmenu directionality",
         "emoticons template paste textcolor"
        ],
        toolbar1: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image",
        toolbar2: "print preview media | forecolor backcolor emoticons",
        image_advtab: true,
        file_browser_callback: RoxyFileBrowser,
        visualblocks_default_state: true,
        //extended_valid_elements: true,
        language: 'tr_TR',
        valid_elements: '*[*]'//,
        //content_css: ['http://revo.targetdigital.com.tr/Content/bootstrap.css', 'http://revo.targetdigital.com.tr/Content/site.css']
    });

    //var edObj = ed.dom.getRoot();

    ////Set the Style "background-image" to the button's color value.
    //ed.dom.setStyle(edObj, 'background-image', "url(some_background_image.jpg)");
});
function RoxyFileBrowser(field_name, url, type, win) {
    var roxyFileman = '/admin/Content/Plugins/fileman/index.html';
    if (roxyFileman.indexOf("?") < 0) {
        roxyFileman += "?type=" + type;
    }
    else {
        roxyFileman += "&type=" + type;
    }
    roxyFileman += '&input=' + field_name + '&value=' + win.document.getElementById(field_name).value;
    if (tinyMCE.activeEditor.settings.language) {
        roxyFileman += '&langCode=' + tinyMCE.activeEditor.settings.language;
    }
    tinyMCE.activeEditor.windowManager.open({
        file: roxyFileman,
        title: 'Dosya Yöneticisi',
        width: 850,
        height: 650,
        resizable: "yes",
        plugins: "media",
        inline: "yes",
        close_previous: "no"
    }, { window: win, input: field_name });
    return false;
}