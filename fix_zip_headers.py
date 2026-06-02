import zipfile
import sys
import os
import shutil

if len(sys.argv) < 2:
    print("Usage: python fix_zip_headers.py <path_to_zip>")
    sys.exit(1)

zip_path = sys.argv[1]
if not os.path.exists(zip_path):
    print(f"Error: file not found: {zip_path}")
    sys.exit(1)

print(f"Fixing zip path separators for: {zip_path}")

temp_zip = zip_path + ".tmp"

try:
    with zipfile.ZipFile(zip_path, 'r') as zin:
        with zipfile.ZipFile(temp_zip, 'w', zipfile.ZIP_DEFLATED) as zout:
            for item in zin.infolist():
                data = zin.read(item.filename)
                new_name = item.filename.replace('\\', '/')
                zout.writestr(new_name, data)
                
    shutil.move(temp_zip, zip_path)
    print("Successfully converted backslashes to forward slashes in zip archive.")
except Exception as e:
    if os.path.exists(temp_zip):
        os.remove(temp_zip)
    print(f"Error fixing zip: {e}")
    sys.exit(1)
