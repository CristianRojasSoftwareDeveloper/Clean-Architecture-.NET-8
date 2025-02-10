-- -- Crear el tipo ENUM LogLevel
-- CREATE TYPE LogLevel AS ENUM ('Trace', 'Debug', 'Information', 'Warning', 'Error', 'Critical');

-- -- Create tables

-- -- Tabla de usuarios
-- CREATE TABLE users (
--     id SERIAL PRIMARY KEY, -- Identificador único de usuario
--     username VARCHAR(20) NOT NULL, -- Nombre de usuario
--     email VARCHAR(50) NOT NULL , -- Correo electrónico
--     name VARCHAR(50) NOT NULL, -- Nombre
--     password VARCHAR(64) NOT NULL, -- Contraseña
--     encrypted_password VARCHAR(64) NOT NULL, -- Contraseña encriptada
--     created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Fecha y hora de creación
--     updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Fecha y hora de última actualización
--     UNIQUE (username) -- Asegura que los nombres de usuario sean únicos
-- );

-- -- Tabla de roles
-- CREATE TABLE roles (
--     id SERIAL PRIMARY KEY, -- Identificador único de rol
--     name VARCHAR(30) NOT NULL, -- Nombre del rol
--     description VARCHAR(80), -- Descripción del rol
--     UNIQUE (name) -- Asegura que los nombres de rol sean únicos
-- );

-- -- Tabla de permisos
-- CREATE TABLE permissions (
--     id SERIAL PRIMARY KEY, -- Identificador único de permiso
--     name VARCHAR(30) NOT NULL, -- Nombre del permiso
--     description VARCHAR(80), -- Descripción del permiso
--     UNIQUE (name) -- Asegura que los nombres de permiso sean únicos
-- );

-- -- Tabla de relación entre usuarios y roles
-- CREATE TABLE user_roles (
--     id SERIAL PRIMARY KEY, -- Identificador único de relación
--     user_id INT NOT NULL, -- ID del usuario
--     role_id INT NOT NULL, -- ID del rol
--     FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE, -- Llave foránea hacia la tabla de usuarios
--     FOREIGN KEY (role_id) REFERENCES roles(id) ON DELETE CASCADE, -- Llave foránea hacia la tabla de roles
--     UNIQUE (user_id, role_id) -- Asegura que la combinación de user_id y role_id sea única
-- );

-- -- Tabla de relación entre roles y permisos
-- CREATE TABLE role_permissions (
--     id SERIAL PRIMARY KEY, -- Identificador único de relación
--     role_id INT NOT NULL, -- ID del rol
--     permission_id INT NOT NULL, -- ID del permiso
--     FOREIGN KEY (role_id) REFERENCES roles(id) ON DELETE CASCADE, -- Llave foránea hacia la tabla de roles
--     FOREIGN KEY (permission_id) REFERENCES permissions(id) ON DELETE CASCADE, -- Llave foránea hacia la tabla de permisos
--     UNIQUE (role_id, permission_id) -- Asegura que la combinación de role_id y permission_id sea única
-- );

-- -- Tabla de registros del sistema
-- CREATE TABLE system_logs (
--     id SERIAL PRIMARY KEY, -- Identificador único del log del sistema
--     log_level LogLevel NOT NULL , -- Nivel de severidad del log
--     source VARCHAR(80) NOT NULL , -- Fuente del evento registrado
--     message VARCHAR(200) NOT NULL , -- Mensaje detallado del evento registrado
--     user_id INT, -- ID del usuario asociado al evento registrado
--     created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Fecha y hora de creación
--     updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Fecha y hora de última actualización
--     FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE SET NULL -- Llave foránea hacia la tabla de usuarios
-- );

-- -- Create indexes

-- -- Índice para buscar roles de un usuario específico
-- CREATE INDEX idx_user_roles_user_id ON user_roles (user_id);

-- -- Índice para buscar usuarios con un rol específico
-- CREATE INDEX idx_user_roles_role_id ON user_roles (role_id);

-- -- Índice para buscar los permisos asociados a un rol específico
-- CREATE INDEX idx_role_permissions_role_id ON role_permissions (role_id);

-- -- Índice para buscar los roles que tienen un permiso específico
-- CREATE INDEX idx_role_permissions_permission_id ON role_permissions (permission_id);

-- -- Índice para buscar registros de logs asociados a un usuario específico
-- CREATE INDEX idx_system_logs_user_id ON system_logs (user_id);

-- -- Insertar datos de prueba en la tabla users
-- INSERT INTO users (username, email, name, password, encrypted_password, created_at, updated_at) VALUES ('usuario1', 'usuario1@example.com', 'Usuario Uno', 'password1', 'hashed_password_1', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
-- INSERT INTO users (username, email, name, password, encrypted_password, created_at, updated_at) VALUES ('usuario2', 'usuario2@example.com', 'Usuario Dos', 'password2', 'hashed_password_2', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

-- -- Insertar datos de prueba en la tabla roles
-- INSERT INTO roles (name) VALUES ('Admin');
-- INSERT INTO roles (name) VALUES ('User');

-- -- Insertar datos de prueba en la tabla permissions
-- INSERT INTO permissions (name) VALUES ('Create');
-- INSERT INTO permissions (name) VALUES ('Read');
-- INSERT INTO permissions (name) VALUES ('Update');
-- INSERT INTO permissions (name) VALUES ('Delete');

-- -- Insertar datos de prueba en la tabla user_roles (relación entre usuarios y roles)
-- INSERT INTO user_roles (user_id, role_id) VALUES (1, 1); -- usuario1 es Admin
-- INSERT INTO user_roles (user_id, role_id) VALUES (2, 2); -- usuario2 es User

-- -- Insertar datos de prueba en la tabla role_permissions (relación entre roles y permisos)
-- INSERT INTO role_permissions (role_id, permission_id) VALUES (1, 1); -- Admin puede Crear
-- INSERT INTO role_permissions (role_id, permission_id) VALUES (1, 2); -- Admin puede Leer
-- INSERT INTO role_permissions (role_id, permission_id) VALUES (1, 3); -- Admin puede Actualizar
-- INSERT INTO role_permissions (role_id, permission_id) VALUES (1, 4); -- Admin puede Eliminar
-- INSERT INTO role_permissions (role_id, permission_id) VALUES (2, 2); -- User puede Leer
