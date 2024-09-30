using FluentMigrator;
using Microsoft.AspNetCore.Http.HttpResults;
using System;

namespace RedMaple.Artifacts.ApiService.Infrastructure.Migrations
{
    [Migration(1)]
    public class Migraiton_001_Initial : Migration
    {
        public override void Down()
        {
            Delete.Table("versions");
            Delete.Table("products");
            Delete.Table("artifacts");
        }

        public override void Up()
        {
            Create.Table("products")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("name").AsString().NotNullable()
                .WithColumn("slug").AsString().NotNullable()
                .WithColumn("icon_url").AsString().Nullable()
                .WithColumn("latest").AsString().Nullable()
                .WithColumn("version_format").AsInt32().NotNullable();

            Create.Table("versions")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("product_id").AsInt64().NotNullable()
                .WithColumn("version").AsString().NotNullable()
                .WithColumn("serialized").AsInt64().NotNullable()
                .WithColumn("flags").AsInt32().NotNullable();

            Create.Table("artifacts")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("product_id").AsInt64().NotNullable()
                .WithColumn("version_id").AsInt64().NotNullable()
                .WithColumn("name").AsString().NotNullable()
                .WithColumn("labels").AsString().Nullable()
                .WithColumn("platform").AsString().Nullable()
                .WithColumn("url").AsString().Nullable()
                .WithColumn("type").AsInt32().NotNullable();
        }
    }
}
