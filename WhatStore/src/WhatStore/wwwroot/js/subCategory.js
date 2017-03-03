(function($){
$.fn.category = function (dropSubCategories) {
    var dropDown = $(this);

    dropDown.on('changes', function () {
        var categoryId = $(this).val();
        dropSubCategories.html('<option value="-1">Selecione a subcategoria</option>');

        $.ajax({
            url: '/product/list/subcategory/' + categoryId,
            method: 'GET',
            success: function (data) {
                $.each(data, function (i, item) {
                    dropSubCategories.append('<option value="' + item.id + '">' + item.name + '<option>');
                });
                dropSubCategories.removeAttr('disabled');
            },
            error: function (x, y, message) {
                dropSubCategories.attr('disable', 'disable');
            }
        });
    });
};

})(jQuery);