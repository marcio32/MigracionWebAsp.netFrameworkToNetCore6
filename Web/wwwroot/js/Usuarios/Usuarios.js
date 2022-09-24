var tablaUsuarios;
$(document).ready(function () {
    debugger
    var token = getCookie('Token');
    tablaUsuarios = $('#usuarios').DataTable({
        ajax: {
            url: 'https://localhost:7008/api/Usuarios/BuscarUsuarios',
            dataSrc: '',
            headers: { "Authorization": "Bearer " + token }
        },
        columns: [
            { data: 'id', title: 'Id' },
            { data: 'nombre', title: 'Nombre' },
            { data: 'apellido', title: 'Apellido' },
            { data: function (row) { return moment(row.Fecha_Nacimiento).format("DD/MM/YYYY") }, title: 'Fecha de nacimiento' },
            { data: 'mail', title: 'Mail' },
            {
                data: function (row) {
                    return row.activo == true ? "Si" : "No";

                },
                title: 'Activo'
            },
            { data: 'roles.nombre', title: 'Activo' },
            {
                data: function (row) {
                    var buttons =
                        `<td><a href='javascript:EditarUsuario(${JSON.stringify(row)})'<i class="fa-solid fa-pen-to-square editarUsuario"></td>` +
                        `<td><a href='javascript:EliminarUsuario(${JSON.stringify(row)})'<i class="fa-solid fa-trash eliminarUsuario"></i></td>`;
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

function GuardarUsuario() {

    $("#usuariosAddPartial").html("");
    $.ajax({
        type: "POST",
        url: "/Usuarios/UsuariosAddPartial",
        data: "",
        contentType: "application/json",
        dataType: "html",
        success: function (result) {
            debugger
            $("#usuariosAddPartial").html(result);
            $('#UsuariosModal').modal('show');
        }
    });
    $('#UsuariosModal').modal('show');

}

function EditarUsuario(row) {
    $("#usuariosAddPartial").html("");
    $.ajax({
        type: "POST",
        url: "/Usuarios/UsuariosAddPartial",
        data: JSON.stringify(row),
        contentType: "application/json",
        dataType: "html",
        success: function (result) {
            $("#usuariosAddPartial").html(result);
            $('#UsuariosModal').modal('show');
        }
    });
    $('#UsuariosModal').modal('show');

}

function EliminarUsuario(row) {
    $.ajax({
        type: "POST",
        url: "/Usuarios/EliminarUsuario",
        data: JSON.stringify(row),
        contentType: "application/json",
        dataType: "html",
        success: function (result) {
            debugger
            tablaUsuarios.ajax.reload();
        }
    });
}

