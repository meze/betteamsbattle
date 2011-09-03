$.fx.speeds._default = 1000;

$(document).ready(function() { 
    


//---------------------------------------------------------------
$(".knopka_1").hover(
  function () {
    $(".knopka_1_hover").show();
  },
  function () {
    $(".knopka_1_hover").hide();
  }
);


$(".knopka_2").hover(
  function () {
    $(".knopka_2_hover").show();
  },
  function () {
    $(".knopka_2_hover").hide();
  }
);

$(".knopka_3").hover(
  function () {
    $(".knopka_3_hover").show();
  },
  function () {
    $(".knopka_3_hover").hide();
  }
);


$(".knopka_4").hover(
  function () {
    $(".knopka_4_hover").show();
  },
  function () {
    $(".knopka_4_hover").hide();
  }
);


$(".knopka_eng").hover(
  function () {
    $(".knopka_eng_hover").show();
  },
  function () {
    $(".knopka_eng_hover").hide();
  }
);



$(".knopka_ros").hover(
  function () {
    $(".knopka_ros_hover").show();
  },
  function () {
    $(".knopka_ros_hover").hide();
  }
);






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
    setEqualHeight($(".content_blok > div"));
    });



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
    setEqualHeight($(".content_blok_b > div"));
    });




}); //Конец ready