var sidebarInstance;

document.addEventListener('DOMContentLoaded', function () {
    sidebarInstance = document.getElementById('sidebar-menu').ej2_instances[0];

    document.getElementById('hamburger').onclick = function () {
        sidebarInstance.toggle();
    };
});

function onMenuItemSelect(args) {
    if (typeof args.item.controller === "undefined") {
        return;
    }
    window.location.href = `/${args.item.controller}/${args.item.action}`;
}