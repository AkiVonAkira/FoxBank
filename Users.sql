ALTER TABLE "public"."bank_user" DROP CONSTRAINT "fk_user_branch";
ALTER TABLE "public"."bank_account" DROP CONSTRAINT "fk_account_currency";
ALTER TABLE "public"."bank_user" DROP CONSTRAINT "fk_user_role";
ALTER TABLE "public"."bank_account" DROP CONSTRAINT "fk_account_user";
DROP TABLE IF EXISTS "public"."bank_account";
DROP TABLE IF EXISTS "public"."bank_branch";
DROP TABLE IF EXISTS "public"."bank_currency";
DROP TABLE IF EXISTS "public"."bank_role";
DROP TABLE IF EXISTS "public"."bank_user";
CREATE TABLE "public"."bank_account" ( 
  "id" SERIAL,
  "name" VARCHAR(30) NOT NULL,
  "interest_rate" NUMERIC NULL,
  "user_id" INTEGER NOT NULL,
  "currency_id" INTEGER NOT NULL,
  "balance" NUMERIC NOT NULL,
  CONSTRAINT "bank_account_pkey" PRIMARY KEY ("id")
);
CREATE TABLE "public"."bank_branch" ( 
  "id" SERIAL,
  "name" VARCHAR(20) NOT NULL,
  CONSTRAINT "bank_branch_pkey" PRIMARY KEY ("id")
);
CREATE TABLE "public"."bank_currency" ( 
  "id" SERIAL,
  "name" VARCHAR(3) NOT NULL,
  "exchange_rate" DOUBLE PRECISION NOT NULL,
  CONSTRAINT "bank_currency_pkey" PRIMARY KEY ("id")
);
CREATE TABLE "public"."bank_role" ( 
  "id" SERIAL,
  "name" VARCHAR(20) NOT NULL,
  "is_admin" BOOLEAN NOT NULL,
  "is_client" BOOLEAN NOT NULL,
  CONSTRAINT "bank_role_pkey" PRIMARY KEY ("id")
);
CREATE TABLE "public"."bank_user" ( 
  "id" SERIAL,
  "first_name" VARCHAR(20) NOT NULL,
  "last_name" VARCHAR(20) NOT NULL,
  "pin_code" VARCHAR(4) NOT NULL,
  "role_id" INTEGER NULL,
  "branch_id" INTEGER NULL,
  CONSTRAINT "bank_user_pkey" PRIMARY KEY ("id")
);
ALTER TABLE "public"."bank_account" DISABLE TRIGGER ALL;
ALTER TABLE "public"."bank_branch" DISABLE TRIGGER ALL;
ALTER TABLE "public"."bank_currency" DISABLE TRIGGER ALL;
ALTER TABLE "public"."bank_role" DISABLE TRIGGER ALL;
ALTER TABLE "public"."bank_user" DISABLE TRIGGER ALL;
INSERT INTO "public"."bank_account" ("id", "name", "interest_rate", "user_id", "currency_id", "balance") VALUES (2, 'Sparkonto', NULL, 1, 1, '1000.00');
INSERT INTO "public"."bank_account" ("id", "name", "interest_rate", "user_id", "currency_id", "balance") VALUES (3, 'Lönekonto', NULL, 1, 1, '2000.00');
INSERT INTO "public"."bank_account" ("id", "name", "interest_rate", "user_id", "currency_id", "balance") VALUES (4, 'Sparkonto', NULL, 2, 2, '500.00');
INSERT INTO "public"."bank_account" ("id", "name", "interest_rate", "user_id", "currency_id", "balance") VALUES (5, 'Lönekonto', NULL, 2, 2, '1000.00');
INSERT INTO "public"."bank_account" ("id", "name", "interest_rate", "user_id", "currency_id", "balance") VALUES (6, 'ISK', NULL, 2, 2, '2000.00');
INSERT INTO "public"."bank_branch" ("id", "name") VALUES (1, 'Stockholm');
INSERT INTO "public"."bank_branch" ("id", "name") VALUES (2, 'Malmö');
INSERT INTO "public"."bank_currency" ("id", "name", "exchange_rate") VALUES (1, 'SEK', 1);
INSERT INTO "public"."bank_currency" ("id", "name", "exchange_rate") VALUES (2, 'USD', 10.29);
INSERT INTO "public"."bank_role" ("id", "name", "is_admin", "is_client") VALUES (1, 'Administrator', true, false);
INSERT INTO "public"."bank_role" ("id", "name", "is_admin", "is_client") VALUES (2, 'Client', false, true);
INSERT INTO "public"."bank_role" ("id", "name", "is_admin", "is_client") VALUES (3, 'ClientAdmin', true, true);
INSERT INTO "public"."bank_user" ("id", "first_name", "last_name", "pin_code", "role_id", "branch_id") VALUES (1, 'Krille', 'P', '1234', 2, 1);
INSERT INTO "public"."bank_user" ("id", "first_name", "last_name", "pin_code", "role_id", "branch_id") VALUES (2, 'Frank', 'E', '1234', 1, 1);
ALTER TABLE "public"."bank_account" ENABLE TRIGGER ALL;
ALTER TABLE "public"."bank_branch" ENABLE TRIGGER ALL;
ALTER TABLE "public"."bank_currency" ENABLE TRIGGER ALL;
ALTER TABLE "public"."bank_role" ENABLE TRIGGER ALL;
ALTER TABLE "public"."bank_user" ENABLE TRIGGER ALL;
ALTER TABLE "public"."bank_account" ADD CONSTRAINT "fk_account_user" FOREIGN KEY ("user_id") REFERENCES "public"."bank_user" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "public"."bank_account" ADD CONSTRAINT "fk_account_currency" FOREIGN KEY ("currency_id") REFERENCES "public"."bank_currency" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "public"."bank_user" ADD CONSTRAINT "fk_user_role" FOREIGN KEY ("role_id") REFERENCES "public"."bank_role" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "public"."bank_user" ADD CONSTRAINT "fk_user_branch" FOREIGN KEY ("branch_id") REFERENCES "public"."bank_branch" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;