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
«license_info.py»
──────────────────────────────
Este módulo centraliza la información de las cabeceras de licencia para archivos fuente de distintos lenguajes,
permitiendo su reutilización y facilitando su mantenimiento.

La información de licencia se genera dinámicamente a partir de un texto base,
adaptándose a los diferentes estilos de comentarios de cada lenguaje.
"""

# Define la información básica de la licencia
LICENSE_BASE_INFO = {
    "project_name": "Clean Architecture - .NET 8",
    "copyright_year": "2025",
    "author_name": "Cristian Rojas Arredondo",
    "author_email": "cristian.rojas.software.engineer@gmail.com",
    "license_url": "https://www.gnu.org/licenses/gpl-3.0.html"
}

# Define los textos base de la licencia (sin comentarios específicos del lenguaje)
LICENSE_TEXT_ENGLISH = """
This file is part of {project_name}.

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program. If not, see <{license_url}>.

Note: In the event of any discrepancy between translations, this version (English) shall prevail.
""".strip()

LICENSE_TEXT_SPANISH = """
Este archivo es parte de {project_name}.

Este programa es software libre: puede redistribuirlo y/o modificarlo
bajo los términos de la Licencia Pública General de GNU publicada por
la Free Software Foundation, ya sea la versión 3 de la Licencia o
(a su elección) cualquier versión posterior.

Este programa se distribuye con la esperanza de que sea útil,
pero SIN NINGUNA GARANTÍA, incluso sin la garantía implícita de
COMERCIABILIDAD o IDONEIDAD PARA UN PROPÓSITO PARTICULAR.
Consulte la Licencia Pública General de GNU para más detalles.

Usted debería haber recibido una copia de la Licencia Pública General de GNU
junto con este programa. De no ser así, véase <{license_url}>.

Nota: En caso de cualquier discrepancia entre las traducciones, la versión en inglés prevalecerá.
""".strip()

LICENSE_TEXT_HEADER = """
{project_name}
Copyright (C) {copyright_year} {author_name}

Author / Contact:
  {author_name} «{author_email}»

A full copy of the GNU GPL v3 is provided in the root of this project in the "LICENSE" file.
Additionally, you can view the license at <{license_url}>.
""".strip()

# Define la metadata para diferentes lenguajes.
LANGUAGES = {
    "csharp": {
        "extension": ".cs",
        "style": {
          "line_comment": "// ",
          "block_start": "/*",
          "block_line": " * ",
          "block_end": " */",
          "region_start": "#region",
          "region_end": "#endregion"
      }
    },
    "python": {
        "extension": ".py",
        "style": {
          "line_comment": "# ",
          "block_start": "#",
          "block_line": "# ",
          "block_end": "#",
          "region_start": "# region",
          "region_end": "# endregion"
        }
    }
    # Se pueden agregar más lenguajes aquí siguiendo el mismo patrón
}

def format_block_text(text, style, indent=""):
    """Formatea un bloque de texto con los estilos de comentario específicos del lenguaje."""
    lines = text.split('\n')
    formatted_lines = []
    
    formatted_lines.append(f"{indent}{style['block_start']}")
    for line in lines:
        if line.strip():
            formatted_lines.append(f"{indent}{style['block_line']}{line}")
        else:
            formatted_lines.append(f"{indent}{style['block_line']}")
    formatted_lines.append(f"{indent}{style['block_end']}")
    
    return '\n'.join(formatted_lines)

def generate_license_info(language_name):
    """Genera la información de licencia para un lenguaje específico."""
    if language_name not in LANGUAGES:
        raise ValueError(f"No se ha definido información para el lenguaje {language_name}")
    
    language_data = LANGUAGES[language_name]
    style = language_data["style"]
    
    # Formatea los textos con la información base
    formatted_header = LICENSE_TEXT_HEADER.format(**LICENSE_BASE_INFO)
    formatted_english = LICENSE_TEXT_ENGLISH.format(**LICENSE_BASE_INFO)
    formatted_spanish = LICENSE_TEXT_SPANISH.format(**LICENSE_BASE_INFO)
    
    # Construye la cabecera completa
    marker = f"{style['region_start']} GPL v3 License Header"
    
    header_parts = [
        marker,
        format_block_text(formatted_header, style),
        "",
        f"{style['region_start']} «English version» GPL v3 License Information ",
        format_block_text(formatted_english, style),
        f"{style['region_end']}",
        "",
        f"{style['region_start']} «Spanish version» GPL v3 License Information",
        format_block_text(formatted_spanish, style),
        f"{style['region_end']}",
        "",
        f"{style['region_end']}"
    ]
    
    header = '\n' + '\n'.join(header_parts)
    
    return {
        "marker": marker,
        "header": header
    }

# ────────────────────────────────────────────────────────────────────────────── #
# Diccionario que mapea la extensión del archivo con su información de licencia.
LICENSE_HEADERS = {}

for language_name in LANGUAGES.keys():
    # Obtiene la extensión directamente de language_data.
    extension = LANGUAGES[language_name]["extension"];
    # Genera la información de licencia para el lenguaje.
    license_info = generate_license_info(language_name);    
    # Agrega la información al diccionario.
    LICENSE_HEADERS[extension] = license_info;