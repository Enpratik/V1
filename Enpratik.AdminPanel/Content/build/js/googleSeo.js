function setGoogleText(that, controlCss) {
    var sourceText = $(that).val();
    if (sourceText.length > 155)
        sourceText = sourceText.substring(1, 155)+"...";
    $("." + controlCss).html(sourceText);
}