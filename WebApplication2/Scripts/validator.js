// validator object

function validator(options) {
    function getParent(element, selector) {
        while (element.parentElement) {
            if (element.parentElement.matches(selector)) {
                return element.parentElement;
            }
            element = element.parentElement;
        }
    }

    var selectorRule = {};

    function validate(inputElement, rule) {
        var errorElement = getParent(inputElement, options.formGroupSelector).querySelector(options.errorSelector);
        var rules = selectorRule[rule.selector];
        var errorMessage;

        // check all the rules ,if false then break the loop
        for (var i = 0; i < rules.length; i++) {
            switch (inputElement.type) {
                case "checkbox":
                case "radio":
                    errorMessage = rules[i](
                        formElement.querySelector(rule.selector + ':checked')
                    );
                    break;
                default:
                    errorMessage = rules[i](inputElement.value);
            }
            if (errorMessage) break;
        }

        if (errorMessage) {
            getParent(inputElement, options.formGroupSelector).classList.add('invalid');
            errorElement.textContent = errorMessage;
        } else {
            errorElement.textContent = '';
            getParent(inputElement, options.formGroupSelector).classList.remove('invalid');
        }

        return !errorMessage;
    }

    var formElement = document.querySelector(options.form);

    if (formElement) {
        formElement.onsubmit = function (e) {
            e.preventDefault();

            var isFormValid = true;

            options.rules.forEach((rule) => {
                var inputElement = formElement.querySelector(rule.selector);
                var isValid = validate(inputElement, rule);
                if (!isValid) {
                    isFormValid = false;
                }
            })


            if (isFormValid) {
                if (typeof options.onSubmit === 'function') {
                    var enableInput = formElement.querySelectorAll('[name]:not([disabled])');

                    var formValue = Array.from(enableInput).reduce((value, input) => {

                        switch (input.type) {
                            case 'checkbox':
                                if (!input.matches(':checked')) {
                                    value[input.name] = '';
                                    return value;
                                };
                                if (!Array.isArray(value[input.name])) {
                                    value[input.name] = [];
                                }
                                value[input.name].push(input.value);
                                break;
                            case 'radio':
                                if (input.matches(':checked')) {
                                    value[input.name] = input.value;
                                }
                                break;
                            case 'file':
                                value[input.name] = input.files;
                            default:
                                value[input.name] = input.value;
                                break;
                        }
                        return value;
                    }, {});

                    options.onSubmit(formValue);
                } else {
                    formElement.submit();
                }
            }
        }

        options.rules.forEach((rule) => {
            // save the rule for each input element
            if (Array.isArray(selectorRule[rule.selector])) {
                selectorRule[rule.selector].push(rule.test);
            } else {
                selectorRule[rule.selector] = [rule.test];
            }

            var inputElements = formElement.querySelectorAll(rule.selector);
            if (inputElements) {
                //handle input blur events
                Array.from(inputElements).forEach((inputElement) => {
                    inputElement.onblur = function () {
                        validate(inputElement, rule);
                    };

                    //handle input change events
                    inputElement.oninput = function () {
                        var errorElement = getParent(inputElement, options.formGroupSelector).querySelector(options.errorSelector);

                        errorElement.textContent = '';
                        getParent(inputElement, options.formGroupSelector).classList.remove('invalid');
                    };
                })

            }
        })
    }
};

//rules rules 
validator.isRequired = (selector, message) => {
    return {
        selector: selector,
        test: function (value) {
            return value ? undefined : message || 'Vui lòng nhập trường này!'
        }
    };
};

validator.isEmail = (selector, message) => {
    return {
        selector: selector,
        test: function (value) {
            var regex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
            return regex.test(value) ? undefined : message || 'email không hợp lệ!'
        }
    };
};

validator.isPatternMatching = (selector, message, regex) => {
    return {
        selector: selector,
        test: function (value) {
            return regex.test(value) ? undefined : message || 'Dữ liệu không hợp lệ'
        }
    };
};

validator.minLength = (selector, min, message) => {
    return {
        selector: selector,
        test: function (value) {
            return value.trim().length >= min ? undefined : message || `Vui lòng đặt mật khẩu tối thiểu ${min} ký tự`
        }
    };
};

validator.equalsTo = (selector, equals, message) => {
    return {
        selector: selector,
        test: function (value) {
            return value.trim().length === equals ? undefined : message || `Vui lòng đặt mật khẩu có ${value} ký tự`
        }
    };
};

validator.minLengthButNotRequired = (selector, min, message) => {
    return {
        selector: selector,
        test: function (value) {
            if (value === "") return undefined;
            return value.trim().length >= min ? undefined : message || `Vui lòng đặt mật khẩu tối thiểu ${min} ký tự`
        }
    };
};

validator.maxLengthButNotRequired = (selector, max, message) => {
    return {
        selector: selector,
        test: function (value) {
            if (value === "") return undefined;
            return value.trim().length <= max ? undefined : message || `Vui lòng đặt mật khẩu tối đa ${min} ký tự`
        }
    };
};

validator.isConfirmed = (selector, getConfirmValue, message) => {
    return {
        selector: selector,
        test: function (value) {
            return value === getConfirmValue() ? undefined : message || 'Giá trị nhập vào không chính xác';
        }
    }
}

validator.isPatternMatchingButNotRequired = (selector, message, regex) => {
    return {
        selector: selector,
        test: function (value) {
            if (value === "") return undefined;
            return regex.test(value) ? undefined : message || 'Dữ liệu không hợp lệ'
        }
    };
}

validator.customValidator = (selector, message, cb) => {
    return {
        selector: selector,
        test: function (value) {
            return cb(value) ? undefined : message || 'Dữ liệu không hợp lệ'
        }
    };
}

//expexted result

//validator({
//    form: '#form-1',
//    errorSelector: '.form-message',
//    formGroupSelector: '.form-group',
//    rules: [
//        validator.isRequired('#fullname', 'Không được để trống'),
//        validator.isRequired('#email', 'Email không được để trống.'),
//        validator.isEmail('#email', 'Email không chính xác'),
//        validator.isRequired('#password', 'Mật khẩu không được để trống.'),
//        validator.minLength('#password', 6),
//        validator.isRequired('input[name="genders"]'),
//        validator.isConfirmed('#password_confirmation', function () {
//            return document.querySelector('#form-1 #password').value;
//        }, 'retype password incorrect')
//    ],
//    onSubmit: function (data) {
//        console.log(data);
//    }
//});

