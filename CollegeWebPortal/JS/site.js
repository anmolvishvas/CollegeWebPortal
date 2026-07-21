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

    // Make report tables responsive on phones (stacked cards)
    makeTablesResponsive();

    // Re-apply after AJAX (UpdatePanel) partial postbacks
    if (window.Sys && Sys.WebForms && Sys.WebForms.PageRequestManager) {
        Sys.WebForms.PageRequestManager.getInstance()
            .add_endRequest(makeTablesResponsive);
    }
});

/* Copies each column header onto its data cells (data-label) so the CSS
   stacked-card layout can show a label in front of every value on mobile.
   Targets report GridViews, which all use the .table-striped class. */
function makeTablesResponsive() {
    $("table.table-striped").each(function () {
        var $table = $(this);
        var headers = [];

        // Grab header text from the first row that contains <th> cells
        $table.find("tr").each(function () {
            var $th = $(this).find("th");
            if ($th.length && headers.length === 0) {
                $(this).addClass("resp-head");
                $th.each(function () {
                    headers.push($(this).text().replace(/\s+/g, " ").trim());
                });
            }
        });

        if (headers.length === 0) { return; }

        // Label only real data rows (cell count matches the header count)
        $table.find("tr").each(function () {
            var $tds = $(this).find("td");
            if ($tds.length === headers.length) {
                $tds.each(function (i) {
                    if (headers[i]) { $(this).attr("data-label", headers[i]); }
                });
            }
        });
    });
}

/* Reusable confirm dialog for delete buttons (wired via OnClientClick) */
function confirmDelete(message) {
    return window.confirm(message || "Are you sure you want to delete this record?");
}
