namespace SINTIA_DWI_ARGANI.wwwroot.js
{
    public class sessionTimeout
    {
        // wwwroot/js/sessionTimeout.js
        $(document).ready(function() {
            var timeoutInMinutes = 30;
            var timeoutInMilliseconds = timeoutInMinutes * 60 * 1000;
            var warningTimeout = timeoutInMilliseconds - (5 * 60 * 1000); // 5 menit sebelum timeout

            var warningTimer = setTimeout(function () {
                showTimeoutWarning();
            }, warningTimeout);

            var timeoutTimer = setTimeout(function () {
                window.location.href = '/Authentication/Logout?timeout=true';
            }, timeoutInMilliseconds);

            function resetTimers() {
                clearTimeout(warningTimer);
                clearTimeout(timeoutTimer);

                warningTimer = setTimeout(function () {
                    showTimeoutWarning();
                }, warningTimeout);

                timeoutTimer = setTimeout(function () {
                    window.location.href = '/Authentication/Logout?timeout=true';
                }, timeoutInMilliseconds);
            }

            function showTimeoutWarning() {
                // Tampilkan modal warning
                $('#sessionTimeoutModal').modal('show');

                // Set timer untuk auto logout jika tidak ada respon
                setTimeout(function () {
                    window.location.href = '/Authentication/Logout?timeout=true';
                }, 5 * 60 * 1000); // 5 menit setelah warning
            }

            // Reset timer pada aktivitas user
            $(document).on('mousemove keydown click scroll', resetTimers);
        });
    }
}
