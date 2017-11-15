using System.Windows.Forms;

namespace ListOfWork.UI.Helpers
{
    public static class FormHelpers
    {
        public static void SetCursor(this Form form, Cursor cursor) => form.Cursor = cursor;
    }
}
