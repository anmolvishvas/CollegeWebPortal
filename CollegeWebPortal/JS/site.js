/* ============================================================
   College Web Portal - client side scripts (jQuery)
   ============================================================ */
$(function () {
    // Auto-dismiss success alerts after 4 seconds
    window.setTimeout(function () {
        $(".alert-auto-dismiss").fadeOut(400);
    }, 4000);

    // Highlight the active sidebar link based on the current URL
    var path = window.location.pathname.toLowerCase();
    $(".app-sidebar .list-group-item").each(function () {
        var href = ($(this).attr("href") || "").toLowerCase();
        if (href && path.indexOf(href.split("/").pop()) !== -1) {
            $(this).addClass("active");
        }
    });
});

/* Reusable confirm dialog for delete buttons (wired via OnClientClick) */
function confirmDelete(message) {
    return window.confirm(message || "Are you sure you want to delete this record?");
}
