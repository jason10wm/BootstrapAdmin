﻿$(function () {
    $('#panelResultHeader').html('查询结果<span class="hidden-400 text-danger">（仅 Administrators 角色成员可删除数据）<span>');
    var bsa = new BootstrapAdmin({
        url: Dicts.url,
        dataEntity: new DataEntity({
            map: {
                ID: "dictID",
                Category: "dictCate",
                Name: "dictName",
                Code: "dictCode",
                Define: "dictDefine"
            }
        })
    });

    $('table').smartTable({
        url: Dicts.url,
        sortName: 'Category',
        queryParams: function (params) { return $.extend(params, { category: $('#txt_dict_cate').val(), name: $("#txt_dict_name").val(), define: $("#txt_dict_define").val() }); },
        columns: [{ checkbox: true },
            { title: "Id", field: "ID", events: bsa.idEvents(), formatter: BootstrapAdmin.idFormatter },
            { title: "字典分项", field: "Category", sortable: true },
            { title: "字典名称", field: "Name", sortable: false },
            { title: "字典代码", field: "Code", sortable: false },
            { title: "字典类别", field: "DefineName", sortable: true }
        ]
    });

    // validate
    $('#dataForm').autoValidate({
        dictCate: {
            required: true,
            maxlength: 50
        },
        dictName: {
            required: true,
            maxlength: 50
        },
        dictCode: {
            required: true,
            maxlength: 50
        },
        dictDefine: {
            required: false,
            maxlength: 50
        }
    });
    // select
    $('select').selectpicker();

    // autocomplete
    $.bc({
        Id: 1, url: Dicts.url, data: { type: 'category' }, swal: false,
        callback: function (result) {
            $('#txt_dict_cate').typeahead({
                source: result,
                autoSelect: true
            });
        }
    });
});