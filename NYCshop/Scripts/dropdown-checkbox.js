function onclickDropdownCheckbox(e) {
    var nodeName = $(e).prop('nodeName');
    var checkbox;

    // tìm tất cả các checkbox của dropdown menu
    if (nodeName == "A") {
        event.stopPropagation();
        checkbox = $(e).find("input[type=checkbox]");
    }
    else {
        if (nodeName == "INPUT") {
            checkbox = $(e);
        }
    }

    // chọn các checkbox thích hợp với từng trường hợp
    if (checkbox.attr("id") == "check-all") {
        if (!checkbox.prop("checked"))
            $("#dropdown-checkbox").find("input[type=checkbox]").each(function () {
                //if ($(this).attr("id") == "select-price" && !$(this).prop("checked")) // xét trường hợp lựa chọn là giá cả
                //    onchangeSelectPrice(this);

                $(this).prop("checked", true);
            });
        else $("#dropdown-checkbox").find("input[type=checkbox]").each(function () {
            //if ($(this).attr("id") == "select-price") // xét trường hợp lựa chọn là giá cả
            //    onchangeSelectPrice(this);

            $(this).prop("checked", false);
        });

    }
    else {
        //if ($(this).attr("id") == "select-price") // xét trường hợp lựa chọn là giá cả
        //    onchangeSelectPrice(this);

        if (checkbox.prop("checked")) {
            checkbox.prop("checked", false);
            if ($("#check-all").prop("checked"))
                $("#check-all").prop("checked", false);
        }
        else {
            checkbox.prop("checked", true);
            if ($("#dropdown-checkbox").find("input[type=checkbox]").length == $("#dropdown-checkbox").find("input[type=checkbox]:checked").length + 1)
                $("#check-all").prop("checked", true);
        } 
    }

    addSearchType();

    return false;
}

function addSearchType() {
    var checkedCheckbox = $("#dropdown-checkbox").find("input[type=checkbox]:checked");
    var searchType = "";
    if (checkedCheckbox.length == 0)
        searchType = "Tên sản phẩm";
    else {
        checkedCheckbox.each(function () {
            searchType += $(this).val() + ",";
        });
        searchType = searchType.substr(0, searchType.length - 1); // xóa dấu phẩy cuối

        // thêm giá vào
        if (searchType.lastIndexOf("Giá") >= 0) {
            var fromPrice = $("#btn-from-price").val();
            var toPrice = $("#btn-to-price").val();
            searchType.replace("Giá", "Giá:" + fromPrice + "-" + toPrice);
        }
    }
    
    $("#SearchType").val(searchType);
}

function onclickOptionButton(e) {
    var dropdownCheckbox = $("#dropdown-checkbox");
    dropdownCheckbox.css({ top: $(e).position().top + $(e).height(), left: $(e).position().left + $(e).width() - dropdownCheckbox.width() });
}

function onclickSearchPrice(e) {
    event.stopPropagation();
}