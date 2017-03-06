(function ($) {

    $.fn.states = function (dropCities) {
        var dropDown = $(this);
        
        dropDown.on('change', function () {
            var stateID = $(this).val();
            dropCities.html('<option value="-1">Selecione sua cidade</option>');

            $.ajax({
                url: '/localization/list/cities/' + stateID,
                method: 'GET',
                success: function (data) {
                    $.each(data, function (i, item) {
                        dropCities.append('<option value="' + item.id + '">' + item.name + '</option>');
                    });

                    dropCities.removeAttr('disabled');
                },
                error: function (x, y, message) {
                    dropCities.attr('disabled', 'disabled');
                }
            });
        });
    };

    $.fn.category = function (dropSubCategories) {
        var dropDown = $(this);

        dropDown.on('change', function () {
            var categoryId = $(this).val();
            dropSubCategories.html('<option value="-1">Selecione a subcategoria</option>');

            $.ajax({
                url: '/product/list/subcategory/' + categoryId,
                method: 'GET',
                success: function (data) {
                    $.each(data, function (i, item) {
                        dropSubCategories.append('<option value="' + item.id + '">' + item.subCategoryName + '<option>');
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