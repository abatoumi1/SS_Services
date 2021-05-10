$(function () {


    $("#EditForm").validate({
        submitHandler: function (form) {
            //submit once validation rules are met
            form.submit();
        },
        rules: {
            FirstName: {
                minlength: 3,
                maxlength: 15,
                required: true
            },
            LastName: {
                minlength: 3,
                maxlength: 15,
                required: true
            },
            Email: {
                minlength: 3,
                maxlength: 15,
                required: true
            },
            Phone: {
                required: true,
                minlength: 25
            },
            State: {
                required: true,
                minlength: 6,
                
            },
            Position: {
                required: true,
               
                
            }
       
        },
        highlight: function (element) {
            var id_attr = "#" + $(element).attr("id") + "1";
            $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
            $(id_attr).removeClass('glyphicon-ok').addClass('glyphicon-remove');
        },
        unhighlight: function (element) {
            var id_attr = "#" + $(element).attr("id") + "1";
            $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
            $(id_attr).removeClass('glyphicon-remove').addClass('glyphicon-ok');
        },
        errorElement: 'span',
        errorClass: 'help-block',
        errorPlacement: function (error, element) {
            if (element.length) {
                error.insertAfter(element);
            } else {
                error.insertAfter(element);
            }
        }
    });
});