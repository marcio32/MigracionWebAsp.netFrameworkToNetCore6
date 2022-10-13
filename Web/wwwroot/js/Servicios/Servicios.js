var tablaServicios;
$(document).ready(function () {
    var token = getCookie('Token');
    tablaServicios = $('#servicios').DataTable({
        ajax: {
            url: 'https://localhost:7008/api/Servicios/BuscarServicios',
            dataSrc: "",
            headers: { "Authorization": "Bearer " + token }
        },
        columns: [
            { data: 'id', title: 'Id' },
            { data: 'nombre', title: 'Nombre' },
            {
                data: function (row) {
                    return row.activo == true ? "Si" : "No"
                }, title: 'Activo'
            },
            {
                data: function (row) {
                    var botones =
                        `<td><a href='javascript:EditarServicio(${JSON.stringify(row)})'><i class="fa-solid fa-pen-to-square editarServicio"></i></td>` +
                        `<td><a href='javascript:EliminarServicio(${JSON.stringify(row)})'><i class="fa-solid fa-trash eliminarServicio"></i></td>`
                        ;
                    return botones;
                }

            }


        ],
        languaje: {
            url: "https://cdn.datatables.net/plug-ins/1.10.19a/i18n/Spanish.json"
        }
    });
});

function GuardarServicio(row) {
    $("#serviciosAddPartial").html("");
    debugger
    $.ajax({
        type: "POST",
        url: "/Servicios/ServiciosAddPartial",
        data: "",
        contentType: "application/json",
        dataType: "html",
        success: function (resultado) {
            debugger
            $("#serviciosAddPartial").html(resultado);
            $('#serviciosModal').modal('show');
        }
    })
}

function EditarServicio(row) {
    $.ajax({
        type: "POST",
        url: "/Servicios/ServiciosAddPartial",
        data: JSON.stringify(row),
        contentType: "application/json",
        dataType: "html",
        success: function (resultado) {
            $("#serviciosAddPartial").html(resultado);
            $('#serviciosModal').modal('show');
        }
    })
}

function EliminarServicio(row) {

    Swal.fire({
        title: '¿Está seguro que desea eliminar el servicio seleccionado?',
        showDenyButton: true,
        confirmButtonText: 'Eliminar',
        denyButtonText: 'Cancelar',
    }).then((resp) => {
        if (resp.isConfirmed) {
            $.ajax({
                type: "POST",
                url: "/Servicios/EliminarServicio",
                data: JSON.stringify(row),
                contentType: "application/json",
                dataType: "html",
                success: function () {
                    tablaServicios.ajax.reload();
                }
            })
        }
    });
}
