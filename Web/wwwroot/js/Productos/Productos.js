var tablaProductos;
$(document).ready(function () {

    tablaProductos = $('#productos').DataTable({
        ajax: {
            url: 'https://localhost:7008/api/Productos/BuscarProductos',
            dataSrc: ''
        },
        columns: [
            { data: 'id', title: 'Id' },
            {
                data: 'imagen',
                render: function (data) {
                    return '<img src="data:image/jpeg;base64,' + data + '"width="100px" height="100px">';
                },
                title: 'Imagen'
            },
            { data: 'descripcion', title: 'Descripcion' },
            { data: 'precio', title: 'Precio' },
            { data: 'stock', title: 'Stock' },
            {
                data: function (row) {
                    return row.activo == true ? "Si" : "No";

                },
                title: 'Activo'
            },
            {
                data: function (row) {
                    var buttons =
                        `<td><a href='javascript:EditarProducto(${JSON.stringify(row)})'<i class="fa-solid fa-pen-to-square editarProducto"></td>` +
                        `<td><a href='javascript:EliminarProducto(${JSON.stringify(row)})'<i class="fa-solid fa-trash eliminarProducto"></i></td>`;
                    return buttons

                },
                title: 'Acciones',
                ordenable: false,
                width: '0%'
            }
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        }
    });

});

function GuardarProducto() {

    $("#productosAddPartial").html("");
    $.ajax({
        type: "POST",
        url: "/Productos/ProductosAddPartial",
        data: "",
        contentType: "application/json",
        dataType: "html",
        success: function (result) {
            debugger
            $("#productosAddPartial").html(result);
            $('#ProductosModal').modal('show');
        }
    });
    $('#ProductosModal').modal('show');

}

function EditarProducto(row) {
    $("#productosAddPartial").html("");
    $.ajax({
        type: "POST",
        url: "/Productos/ProductosAddPartial",
        data: JSON.stringify(row),
        contentType: "application/json",
        dataType: "html",
        success: function (result) {
            $("#productosAddPartial").html(result);
            $('#ProductosModal').modal('show');
        }
    });
    $('#ProductosModal').modal('show');

}

function EliminarProducto(row) {
    $.ajax({
        type: "POST",
        url: "/Productos/EliminarProducto",
        data: JSON.stringify(row),
        contentType: "application/json",
        dataType: "html",
        success: function (result) {
            debugger
            tablaProductos.ajax.reload();
        }
    });
}
