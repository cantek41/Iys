$(document).ready(function () {

    // Adds title attributes and classnames to list items
    $("ul#nav li a:contains('itim')").addClass("egitim").attr('title', 'Eğitim İçeriği');
    $("ul#nav li a:contains('Kurslar')").addClass("dashboard").attr('title', 'Kurslar');
    $("ul#nav li a:contains('mler')").addClass("pages").attr('title', 'Bölümler');
    $("ul#nav li a:contains('Dersler')").addClass("dersler").attr('title', 'Dersler');
    $("ul#nav li a:contains('Belgeler')").addClass("belgeler").attr('title', 'Belgeler');
    $("ul#nav li a:contains('Sorular')").addClass("widgets").attr('title', 'Sorular');
    $("ul#nav li a:contains('Ekle')").addClass("search").attr('title', 'Cevap Ekle');
    $("ul#nav li a:contains('Sonucu')").addClass("trash").attr('title', 'Cevap Sonucu');
    $("ul#nav li a:contains('Anasayfa')").addClass("dashboard").attr('title', 'Anasayfa');

    // Animate sidebar navigation

    $('ul#nav li:has(ul)').hover(function () {
        $('.flyoutBg').stop().animate({ left: '175px' }, 300);
        $(this).find('ul').stop().animate({ 'left': '175px' }, 300);
    }, function () {
        $('.flyoutBg').stop().animate({ left: '25px' }, 300);
        $(this).find('ul').stop().animate({ 'left': '25px' }, 300);
    });
});