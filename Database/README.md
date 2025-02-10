# Manual de Uso: PostgreSQL Contenerizado con Docker y Docker Compose

Este manual describe cómo configurar, construir y administrar una instancia de base de datos **PostgreSQL** contenerizada utilizando **Docker** y **Docker Compose**. La estructura del proyecto incluye archivos esenciales como `Dockerfile`, `docker-compose.yml` y `.env` para una fácil configuración y despliegue.

## Estructura de Archivos

```
.
├── .env                         # Variables de entorno para configurar PostgreSQL
├── db-container_initializer.sql # Script opcional de inicialización de la base de datos
├── docker-compose.yml           # Configuración de servicios Docker Compose
├── Dockerfile                   # Definición de la imagen de PostgreSQL
└── README.md                    # Manual de uso (este archivo)
```

---

## 1. **Requisitos Previos**

Antes de empezar, asegúrate de tener instalado:

- **Docker**: https://docs.docker.com/get-docker/
- **Docker Compose**: https://docs.docker.com/compose/install/

Verifica las versiones instaladas ejecutando:
```bash
docker --version
docker-compose --version
```

---

## 2. **Instrucciones de Uso**

### **2.1 Construir la Imagen Docker**

Para construir la imagen de PostgreSQL, navega a la carpeta del proyecto y ejecuta:

```bash
docker-compose build
```

Esto creará una imagen Docker con la configuración definida en el `Dockerfile`.

---

### **2.2 Iniciar los Contenedores**

Para iniciar el contenedor de la base de datos, usa el siguiente comando:

```bash
docker-compose up -d
```

- **`-d`**: Ejecuta el contenedor en segundo plano.
- Docker descargará la imagen base si aún no está presente y aplicará las configuraciones definidas en `docker-compose.yml` y `.env`.

Verifica que el contenedor esté corriendo:
```bash
docker ps
```

---

### **2.3 Detener los Contenedores**

Para detener el contenedor sin eliminarlo, usa:
```bash
docker-compose stop
```

---

### **2.4 Eliminar Contenedores y Volúmenes**

Si deseas detener y eliminar los contenedores y los volúmenes creados, ejecuta:

```bash
docker-compose down -v
```

- **`-v`**: Elimina también los volúmenes.

---

### **2.5 Ver Logs del Contenedor**

Para visualizar los logs generados por PostgreSQL en tiempo real, usa:
```bash
docker-compose logs -f
```

- **`-f`**: Sigue los logs en tiempo real.

---

### **2.6 Conectarse a la Base de Datos PostgreSQL**

Puedes conectarte al contenedor PostgreSQL utilizando `psql`. Por ejemplo:

1. Abre una terminal dentro del contenedor:
   ```bash
   docker exec -it database-service_container bash
   ```

2. Accede a PostgreSQL con el usuario y base de datos configurados:
   ```bash
   psql -U db_admin -d web_system_db
   ```

---

## 3. **Comandos Rápidos**
-------------------------------------------------------------------------
| Acción                            | Comando                           |
|-----------------------------------|-----------------------------------|
| Construir la imagen               | `docker-compose build`            |
| Iniciar los contenedores          | `docker-compose up -d`            |
| Detener los contenedores          | `docker-compose stop`             |
| Eliminar contenedores y volúmenes | `docker-compose down -v`          |
| Ver logs en tiempo real           | `docker-compose logs -f`          |
| Acceder al contenedor             | `docker exec -it <nombre> bash`   |
-------------------------------------------------------------------------

---

## 4. **Notas Importantes**

- El directorio `/var/lib/postgresql/data` dentro del contenedor está mapeado a un volumen llamado `postgre-sql_data` para garantizar la persistencia de datos entre reinicios del contenedor.
- Si deseas inicializar la base de datos con un script SQL, descomenta la línea correspondiente en `docker-compose.yml`:
  ```yaml
  - ./db-container_initializer.sql:/docker-entrypoint-initdb.d/database_initializer.sql
  ```
  Asegúrate de que el archivo SQL esté en la raíz del proyecto.

---

## 5. **Referencias**

- [Documentación de Docker](https://docs.docker.com/)
- [Documentación de Docker Compose](https://docs.docker.com/compose/)
- [Documentación de PostgreSQL](https://www.postgresql.org/docs/)

---

¡Listo! Ahora tienes una base de datos PostgreSQL funcional y contenida utilizando Docker y Docker Compose.
