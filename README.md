# CreateScripter
SQLServerの定義を出力します

## Arguments

```
usage: CreateScripter
	 {-server サーバ名}
	 {-db データベース名}
	 [-user ユーザ名]
	 [-pass パスワード名]
	 [-obj DBオブジェクト名(部分一致)]
	 [-out 出力ファイル名]
	 [-kind DB種類(t:テーブル,v:ビュー,sp:ストアドプロシージャ,uf:ユーザ定義関数,udt:ユーザ定義データ型,s:シノニム)]

```
## Example
### 全てのDBオブジェクトを出力する場合
```
CreateScripter -server testserver -testdb -user testuser -pass password
```
### 全てのテーブルのみ出力する場合
```
CreateScripter -server testserver -testdb -user testuser -pass password -kind t
```
### 任意のファイルに出力する場合
```
CreateScripter -server testserver -testdb -user testuser -pass password -out scripts.sql
```
