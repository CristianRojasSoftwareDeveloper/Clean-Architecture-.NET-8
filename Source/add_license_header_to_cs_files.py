#!/usr/bin/env python3;
# -*- coding: utf-8 -*-;

"""
Agrega una cabecera de licencia a todos los archivos C# en un proyecto.

Este script recorre recursivamente el directorio especificado, omitiendo las carpetas
"obj" y "bin". Para cada archivo con extensión ".cs", se verifica si la cabecera de licencia
definida en "LICENSE_HEADER" ya está presente. Si no lo está, la agrega al inicio del archivo.
"""

import os

# Define la cabecera de licencia como constante «LICENSE_HEADER».
LICENSE_HEADER = """#region GPL v3 License Header

/*
 * Clean Architecture - .NET 8
 * Copyright (C) 2025 Cristian Rojas Arredondo
 *
 * Author / Contact:
 *   Cristian Rojas Arredondo «cristian.rojas.software.developer@gmail.com»
 *
 * A full copy of the GNU GPL v3 is provided in the root of this project in the "LICENSE" file.
 * Additionally, you can view the license at <https://www.gnu.org/licenses/gpl-3.0.html>.
 */

#region «English version» GPL v3 License Information 
/*
 * This file is part of Clean Architecture - .NET 8.
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <https://www.gnu.org/licenses/gpl-3.0.html>.
 * 
 * Note: In the event of any discrepancy between translations, this version (English) shall prevail.
 */
#endregion

#region «Spanish version» GPL v3 License Information
/*
 * Este archivo es parte de Clean Architecture - .NET 8.
 *
 * Este programa es software libre: puede redistribuirlo y/o modificarlo
 * bajo los términos de la Licencia Pública General de GNU publicada por
 * la Free Software Foundation, ya sea la versión 3 de la Licencia o
 * (a su elección) cualquier versión posterior.
 *
 * Este programa se distribuye con la esperanza de que sea útil,
 * pero SIN NINGUNA GARANTÍA, incluso sin la garantía implícita de
 * COMERCIABILIDAD o IDONEIDAD PARA UN PROPÓSITO PARTICULAR.
 * Consulte la Licencia Pública General de GNU para más detalles.
 *
 * Usted debería haber recibido una copia de la Licencia Pública General de GNU
 * junto con este programa. De no ser así, véase <https://www.gnu.org/licenses/gpl-3.0.html>.
 * 
 * Nota: En caso de cualquier discrepancia entre las traducciones, la versión en inglés prevalecerá.
 */
#endregion

#endregion
""";

def add_license_header_to_cs_files(root_directory: str) -> None:
    """
    Agrega la cabecera de licencia a todos los archivos con extensión ".cs"
    dentro del directorio especificado, omitiendo las carpetas "obj" y "bin".

    Parámetros:
        «root_directory» (str): Ruta del directorio raíz a procesar.
    """
    # Recorre recursivamente el directorio raíz y obtiene subdirectorios y archivos
    for current_directory, subdirectories, files in os.walk(root_directory):
        # Excluye carpetas "obj" y "bin" para evitar archivos compilados o temporales
        subdirectories[:] = [directory for directory in subdirectories if directory not in ("obj", "bin")];

        # Itera sobre los archivos en el directorio actual
        for file_name in files:
            # Verifica que el archivo tenga extensión ".cs", si no, continúa con el siguiente
            if not file_name.lower().endswith(".cs"):
                continue;

            # Construye la ruta completa del archivo
            full_path = os.path.join(current_directory, file_name);

            # Obtiene la ruta relativa para mostrarla en la salida (hace más legible el mensaje)
            relative_path = f"...{os.sep}{os.path.relpath(full_path, root_directory)}";

            try:
                # Abre el archivo en modo lectura y obtiene su contenido
                with open(full_path, "r", encoding="utf-8") as file:
                    file_content = file.read();
            except Exception as read_error:
                # Si hay un error al leer el archivo, muestra un mensaje y pasa al siguiente
                print(f"❌ No se pudo leer {relative_path}: {read_error}");
                continue;

            # Verifica si la cabecera de licencia ya está presente en el archivo
            if "#region GPL v3 License Header" in file_content:
                # Si la cabecera ya existe, notifica y continúa con el siguiente archivo
                print(f"✔️ Licencia ya presente en {relative_path}");
                continue;

            # Si la cabecera no está presente, notifica que se agregará
            print(f"➕ Agregando licencia a {relative_path}");

            try:
                # Abre el archivo en modo escritura y agrega la cabecera al inicio
                with open(full_path, "w", encoding="utf-8") as file:
                    file.write(LICENSE_HEADER + "\n" + file_content);
            except Exception as write_error:
                # Si hay un error al escribir en el archivo, muestra un mensaje de error
                print(f"❌ No se pudo escribir en {relative_path}: {write_error}");

if __name__ == "__main__":
    # Obtiene la ruta del directorio donde se encuentra este script
    base_directory = os.path.dirname(os.path.abspath(__file__));
    # Invoca a la función para agregar la cabecera de licencia en los archivos C#
    add_license_header_to_cs_files(base_directory);
    # Imprime un mensale en consola indicando el fin del proceso.
    print("✅ Proceso completado.");
