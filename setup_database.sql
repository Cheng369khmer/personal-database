-- ============================================================
--  MyPortfolio Database Setup for XAMPP (MySQL)
--  Run this in phpMyAdmin or MySQL command line
-- ============================================================

-- Step 1: Create the database
CREATE DATABASE IF NOT EXISTS myportfolio_db
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE myportfolio_db;

-- ============================================================
-- NOTE: The tables below are created automatically by
-- Entity Framework Core migrations when you run:
--
--   dotnet ef migrations add InitialCreate
--   dotnet ef database update
--
-- This SQL file is provided as a REFERENCE / BACKUP only.
-- ============================================================

-- Verify database was created
SHOW DATABASES LIKE 'myportfolio_db';
SELECT 'Database myportfolio_db created successfully!' AS status;
