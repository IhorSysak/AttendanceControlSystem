namespace AttendanceControlSystem.Utility
{
    public static class ItemSerializer
    {
        public static byte[] Serialize(IFormFile formFile) 
        {
            if (formFile != null) 
            {
                using (var memory = new MemoryStream()) 
                {
                    formFile.CopyTo(memory);
                    var fileBytes = memory.ToArray();

                    return fileBytes;
                }
            }

            return new byte[0];
        }
    }
}
