$.fx.speeds._default = 1000;

$(document).ready(function() { 
    




    function setEqualHeight(columns)
    {
    var tallestcolumn = 0;
    columns.each(
    function()
    {
    currentHeight = $(this).height();
    if(currentHeight > tallestcolumn)
    {
    tallestcolumn = currentHeight;
    }
    }
    );
    columns.height(tallestcolumn);
    }
    $(document).ready(function() {
    setEqualHeight($(".content_blok > div, .content_blok_b > div"));
    });








 $(".knopka_st").hover(function () {
      $(this).toggleClass("ff");
    });


}); //Конец ready