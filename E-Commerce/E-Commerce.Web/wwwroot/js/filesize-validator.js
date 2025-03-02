$.validator.addMethod('fileSizeValidation', function (value, element, parameter) {
    return this.optional(element) || element.files[0].size <= parameter;
});
