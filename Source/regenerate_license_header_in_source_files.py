#!/usr/bin/env python3
# -*- coding: utf-8 -*-

# region GPL v3 License Header
#
# Clean Architecture - .NET 8
# Copyright (C) 2025 Cristian Rojas Arredondo
# 
# Author / Contact:
#   Cristian Rojas Arredondo «cristian.rojas.software.engineer@gmail.com»
# 
# A full copy of the GNU GPL v3 is provided in the root of this project in the "LICENSE" file.
# Additionally, you can view the license at <https://www.gnu.org/licenses/gpl-3.0.html>.
#

# region «English version» GPL v3 License Information 
#
# This file is part of Clean Architecture - .NET 8.
# 
# This program is free software: you can redistribute it and/or modify
# it under the terms of the GNU General Public License as published by
# the Free Software Foundation, either version 3 of the License or
# (at your option) any later version.
# 
# This program is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
# See the GNU General Public License for more details.
# 
# You should have received a copy of the GNU General Public License
# along with this program. If not, see <https://www.gnu.org/licenses/gpl-3.0.html>.
# 
# Note: In the event of any discrepancy between translations, this version (English) shall prevail.
#
# endregion

# region «Spanish version» GPL v3 License Information
#
# Este archivo es parte de Clean Architecture - .NET 8.
# 
# Este programa es software libre: puede redistribuirlo y/o modificarlo
# bajo los términos de la Licencia Pública General de GNU publicada por
# la Free Software Foundation, ya sea la versión 3 de la Licencia o
# (a su elección) cualquier versión posterior.
# 
# Este programa se distribuye con la esperanza de que sea útil,
# pero SIN NINGUNA GARANTÍA, incluso sin la garantía implícita de
# COMERCIABILIDAD o IDONEIDAD PARA UN PROPÓSITO PARTICULAR.
# Consulte la Licencia Pública General de GNU para más detalles.
# 
# Usted debería haber recibido una copia de la Licencia Pública General de GNU
# junto con este programa. De no ser así, véase <https://www.gnu.org/licenses/gpl-3.0.html>.
# 
# Nota: En caso de cualquier discrepancia entre las traducciones, la versión en inglés prevalecerá.
#
# endregion

# endregion

"""
«regenerate_license_header_in_source_files.py»
────────────────────────────────────────────────────────────
Este script recorre recursivamente el directorio padre del archivo actual e inserta o regenera la cabecera
de licencia en archivos fuente con extensión «.cs» (C#) y «.py» (Python).

Modos de operación:
    • Modo "insert-only": Solo agrega la cabecera en archivos que aún no la contienen.
    • Modo "full-regeneration": Inserta la cabecera en archivos sin ella y reemplaza (regenera) la cabecera
                                en aquellos archivos que ya la contienen pero se encuentran desactualizados.

La configuración de la licencia (propiedades «extension_type», «marker» y «header») se importa
desde el módulo «license_info.py», centralizando su definición.

Uso desde línea de comando:
    regenerate_license_header_in_source_files.py --mode {insert-only,full-regeneration}
"""

import os
import argparse
from typing import List, Optional, Tuple
from license_info import LICENSE_HEADERS  # Diccionario que define el marcador y la cabecera para cada tipo de archivo.

def is_special_line(line: str) -> bool:
    """
    «Verifica si una línea es especial» (shebang, codificación o configuración de vim).

    Argumentos:
        «line» (str): Línea a verificar.

    Retorna:
        «bool»: True si es una línea especial, False en caso contrario.
    """
    stripped_line = line.strip()
    return (
        stripped_line.startswith("#!") or
        stripped_line.startswith("# -*-") or
        stripped_line.startswith("# vim:")
    )

def is_comment_line(line: str) -> bool:
    """
    «Determina si una línea forma parte de un bloque de cabecera».
    Se evalúa si la línea está vacía o comienza con un símbolo de comentario típico
    para archivos «.cs» y «.py».

    Argumentos:
        «line» (str): Línea a evaluar.
    
    Retorna:
        «bool»: True si la línea es vacía o de comentario; False en caso contrario.
    """
    # Elimina espacios a la izquierda para evaluar el inicio de la línea.
    stripped_line = line.lstrip()
    return stripped_line == "" or stripped_line.startswith(("#", "/*", "*", "//"))

def extract_existing_header(file_content: str, header_marker: str) -> Tuple[Optional[str], str, List[str]]:
    """
    «Extrae el bloque de cabecera existente» en el archivo, si comienza con el «header_marker» especificado,
    y detecta las líneas iniciales especiales (shebang y codificación).

    Argumentos:
        «file_content» (str): Contenido completo del archivo.
        «header_marker» (str): Marcador que identifica el inicio de la cabecera de licencia.

    Retorna:
        tuple: Una tupla («existing_header», «content», «preserved_lines») donde:
            - «preserved_lines» (List[str]): Lista de líneas especiales (shebang y codificación) que se deben preservar.
            - «existing_header» (Optional[str]): Bloque de cabecera encontrado (incluye líneas vacías y de comentario).
              Si no se encuentra la cabecera, será None.
            - «content» (str): Resto del contenido del archivo (sin la cabecera ni las líneas especiales).
    """
    # Divide el contenido en líneas conservando los saltos de línea.
    lines = file_content.splitlines(keepends=True)
    
    # Detecta y preserva las líneas especiales (por ejemplo, shebang o codificación).
    preserved_lines = []
    header_start_index = 0
    for index, line in enumerate(lines):
        if is_special_line(line):
            preserved_lines.append(line)
            header_start_index = index + 1
        else:
            break

    # Considera el contenido a partir de las líneas especiales.
    content_lines = lines[header_start_index:]
    
    # Variable que almacenará el índice donde comienza la cabecera
    header_start = None

    # Bandera para detectar si hay contenido de código antes del marcador de cabecera
    found_non_empty_line = False

    # Recorremos todas las líneas del contenido
    for index, line in enumerate(content_lines):
        stripped_line = line.strip()  # Eliminamos espacios en blanco de la línea

        # Si la línea está vacía, seguimos buscando el `header_marker`, pero solo si no hemos encontrado código antes
        if stripped_line == "":
            if found_non_empty_line:
                break  # Si ya encontramos código antes, detenemos la búsqueda porque la cabecera no debe estar separada
            continue  # Si aún no hay código, permitimos más líneas vacías antes de la cabecera

        # Si encontramos el `header_marker`, guardamos el índice y terminamos la búsqueda
        if stripped_line.startswith(header_marker):
            header_start = index
            break

        # Si la línea no está vacía ni es la cabecera, significa que hay código antes del `header_marker`
        found_non_empty_line = True

    # Si no se encontró un marcador de cabecera válido, asumimos que no hay cabecera
    if header_start is None:
        return preserved_lines, None, "".join(content_lines)  
        # Retorna:
        # - `preserved_lines`: Las líneas especiales detectadas (ej. shebang, codificación).
        # - `None`: Indica que no hay cabecera existente.
        # - `content_lines`: El contenido del archivo sin modificaciones.

    # Extrae las líneas que conforman el bloque de cabecera.
    header_lines = []
    header_end_index = 0
    for line in content_lines:
        if is_comment_line(line):
            header_lines.append(line)
            header_end_index += 1
        else:
            break

    existing_header = "".join(header_lines)
    content = "\n" + "".join(content_lines[header_end_index:])
    return preserved_lines, existing_header, content

def regenerate_license_headers(root_directory: str, mode: str) -> None:
    """
    «Recorre el directorio» «root_directory» y procesa cada archivo fuente con extensión «.cs» y «.py».
    Dependiendo del «mode» especificado:
      - "insert-only": Solo inserta la cabecera si no está presente.
      - "full-regeneration": Reemplaza la cabecera si está desactualizada.

    Argumentos:
        «root_directory» (str): Directorio raíz desde donde se inicia la búsqueda recursiva.
        «mode» (str): Modo de operación ("insert-only" o "full-regeneration").
    """
    # Directorios a excluir (carpetas de compilación o entornos virtuales).
    excluded_dirs = {"obj", "bin", "venv", "env", "__pycache__"}

    # Recorre recursivamente el directorio raíz.
    for current_dir, subdirs, files in os.walk(root_directory):
        # Excluye directorios irrelevantes
        subdirs[:] = [d for d in subdirs if d not in excluded_dirs]

        for file_name in files:
            file_extension = os.path.splitext(file_name)[1].lower()
            # Procesa solo archivos con extensiones definidas en «LICENSE_HEADERS».
            if file_extension not in LICENSE_HEADERS:
                continue

            full_file_path = os.path.join(current_dir, file_name)
            relative_file_path = f"...{os.sep}{os.path.relpath(full_file_path, root_directory)}"

            # Intenta leer el contenido original del archivo.
            try:
                with open(full_file_path, "r", encoding="utf-8") as source_file:
                    original_content = source_file.read()
            except Exception as read_error:
                print(f"❌ No se pudo leer {relative_file_path}: {read_error}")
                continue

            # Obtiene la configuración de la licencia según la extensión del archivo.
            license_config = LICENSE_HEADERS[file_extension]
            header_marker = license_config["marker"]
            new_license_header = license_config["header"]

            # Extrae la cabecera existente (si la hay) y las líneas especiales que se deben preservar.
            preserved_lines, existing_header, content = extract_existing_header(original_content, header_marker)

            # Bandera para determinar si se debe actualizar el archivo y variable para el nuevo contenido.
            file_updated = False
            updated_content = ""

            if existing_header is None:
                # No existe cabecera: se inserta la nueva cabecera.
                print(f"➕ Insertando «License Header» en «{relative_file_path}».")
                updated_content = "".join(preserved_lines) + new_license_header + "\n" + content
                file_updated = True
            else:
                if mode == "insert-only":
                    # En modo 'insert-only', si la cabecera ya existe, no se realiza ninguna modificación.
                    print(f"✔️ El «License Header» en «{relative_file_path}» ya está presente (modo insert-only)")
                    file_updated = False
                elif mode == "full-regeneration":
                    # En modo 'full-regeneration', se regenera la cabecera solo si la existente difiere de la nueva.
                    if existing_header.strip() == new_license_header.strip():
                        print(f"✔️ El «License Header» en «{relative_file_path}» se encuentra actualizado.")
                        file_updated = False
                    else:
                        print(f"🔄 Regenerando «License Header» en «{relative_file_path}».")
                        updated_content = "".join(preserved_lines) + new_license_header + "\n" + content
                        file_updated = True

            # Se sobrescribe el archivo únicamente si se realizó alguna modificación.
            if file_updated:
                try:
                    with open(full_file_path, "w", encoding="utf-8") as target_file:
                        target_file.write(updated_content)
                except Exception as write_error:
                    print(f"❌ No se pudo escribir en {relative_file_path}: {write_error}")

def parse_arguments():
    """
    «Parsea y valida los argumentos de línea de comando» para el script.

    Este método configura el argumento «mode», que determina el comportamiento del script
    al procesar las cabeceras de licencia en los archivos fuente.

    Argumentos:
        --mode (str): Define el modo de operación del script:
            - «insert-only»: Solo inserta la cabecera de licencia en archivos que aún no la contienen.
                             No modifica las cabeceras ya existentes.
            - «full-regeneration»: Inserta la cabecera en archivos sin ella y reemplaza las cabeceras
                                   desactualizadas en aquellos archivos que ya la contienen.

    Retorna:
        «argparse.Namespace»: Objeto que contiene el valor del argumento «mode».
                                Se accede a este valor mediante «args.mode».
    """
    parser = argparse.ArgumentParser(
        description="Regenera o inserta la cabecera de licencia en archivos fuente (.cs y .py)."
    )
    
    parser.add_argument(
        "--mode",
        choices=["insert-only", "full-regeneration"],
        default="insert-only",
        help=("Modo de operación: 'insert-only' solo agrega la cabecera en archivos sin ella; "
              "'full-regeneration' reemplaza cabeceras desactualizadas y agrega en archivos sin ella.")
    )
    
    return parser.parse_args()

if __name__ == "__main__":
    # Obtiene el directorio del script actual.
    script_directory = os.path.dirname(os.path.abspath(__file__))

    # Define el directorio raíz como la carpeta padre del script actual.
    root_dir = os.path.abspath(os.path.join(script_directory, os.pardir))

    # Parsea los argumentos de línea de comando.
    args = parse_arguments()
    
    # Ejecuta la regeneración/inserción de cabeceras de licencia.
    regenerate_license_headers(root_dir, args.mode)
    
    # Mensaje de confirmación de finalización.
    print("✅ La regeneración de las «License Header's» en los archivos del código fuente ha finalizado correctamente.")
