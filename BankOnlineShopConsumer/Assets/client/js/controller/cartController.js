var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $("#btnContinue").off('click').on('click', function () {
            window.location.href = "/";
        });

        $("#btnPayment").off('click').on('click', function () {
            window.location.href = "/thanh-toan";
        });

        $("#btnUpdate").off('click').on('click', function () {
            var listProduct = $(".txtQuantity");
            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),
                    Product: {
                        ID: $(item).data('id')
                    }
                });
            });

            $.ajax({
                url: 'Cart/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });

        $("#btnDeleteAll").off('click').on('click', function () {

            $.ajax({
                url: 'Cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });

        $(".btn-delete").off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                data: { id: $(this).data('id') },
                url: 'Cart/Delete',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });

        $("#payOnline").off('click').on('click', function (e) {
            var shipname = $(".shipname").val();
            var mobile = $(".mobile").val();
            var address = $(".address").val();
            var email = $(".email").val();
            var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;

            if (shipname.length < 4) {
                $(".shipname").val("");
                $(".shipname").attr("placeholder", "Tên người nhận phải lớn hơn 4 kí tự.");
                $(".shipname").addClass('placehoder');
            }
            if (shipname == '') {
                $(".shipname").val("");
                $(".shipname").attr("placeholder", "Bạn chưa nhập tên người nhận.");
                $(".shipname").addClass('placehoder');
            }

            if (mobile == '') {
                $(".mobile").val("");
                $(".mobile").attr("placeholder", "Bạn chưa nhập số điện thoại.");
                $(".mobile").addClass('placehoder');
            }
            else if (mobile.length != 10) {
                $(".mobile").val("");
                $(".mobile").attr("placeholder", "Số điện thoại phải chứa 10 kí tự.");
                $(".mobile").addClass('placehoder');
            }

            if (address.length < 5) {
                $(".address").val("");
                $(".address").attr("placeholder", "Địa chỉ phải lớn hơn 5 kí tự.");
                $(".address").addClass('placehoder');
            } else if (address == '') {
                $(".address").val("");
                $(".address").attr("placeholder", "Bạn chưa nhập địa chỉ người nhận");
                $(".address").addClass('placehoder');
            }

            if (email == '') {
                $(".email").val("");
                $(".email").attr("placeholder", "Bạn chưa nhập email người nhận.");
                $(".email").addClass('placehoder');
            } else if (regex.test(email) == '') {
                $(".email").val("");
                $(".email").attr("placeholder", "Email không hợp lệ.");
                $(".email").addClass('placehoder');
            }
            if ($('.partnerAccount').val()=='') {
                $('.partnerAccount').attr("placeholder", "Bạn chưa nhập số tài khoản.");
                $(".partnerAccount").addClass('placehoder');
            }

            if ($('.paswordPartner').val() == '') {
                $('.paswordPartner').attr("placeholder", "Bạn chưa nhập password.");
                $(".paswordPartner").addClass('placehoder');
            }

            if (shipname == '' || shipname.length < 4
                || mobile.length != 10 || mobile == ''
                || address == '' || address.length < 5
                || email == '' || regex.test(email) == ''
            ) {

            }
            else {
                e.preventDefault();
                $.ajax({
                    data: {
                        Account: $('.partnerAccount').val(),
                        Password: $('.paswordPartner').val()
                    },
                    url: 'PayLogin/Login',
                    dataType: 'json',
                    type: 'POST',
                    success: function (res) {
                        if (res.status == true) {
                            $.ajax({
                                data: {
                                    shipname: $(".shipname").val(),
                                    mobile: $(".mobile").val(),
                                    address: $(".address").val(),
                                    email: $(".email").val()
                                },
                                url: 'PayLogin/SaveOrder',
                                dataType: 'json',
                                type: 'POST',
                                success: function (res) {
                                    if (res.status == true) {
                                        window.location.href = "/thanh-toan-qua-pay";
                                    }
                                }
                            });
                        }
                        if (res.status == false) {
                            alert("Pay: Số tài khoản hoặc password không đúng.");
                        }
                    }
                });

            }
        });
    }
}
cart.init();