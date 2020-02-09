delimiter ;

create table Guild (
	ID int not null auto_increment,
	DiscordID bigint not null unique key,
	primary key (ID)
);

create table GuildLocale (
	ID int not null,
	Locale tinyint not null,
	primary key (ID),
	foreign key (ID) references Guild(ID)
);

create table GuildPrefix (
	ID int not null,
	Prefix varchar(10) not null,
	primary key (ID),
	foreign key (ID) references Guild(ID)
);

delimiter $$

create procedure GetPrefix (
	in _ID bigint
) begin
	select GuildPrefix.Prefix from GuildPrefix join Guild on Guild.ID = GuildPrefix.ID where Guild.DiscordID = _ID;
end$$

create procedure GetLocale (
	in _ID bigint
) begin
	select GuildLocale.Locale from GuildLocale join Guild on Guild.ID = GuildLocale.ID where Guild.DiscordID = _ID;
end$$

create procedure SetPrefix (
	in _ID bigint,
	in _Prefix varchar(10)
) begin
	declare GuildID int;
	set GuildID = (select ID from Guild where DiscordID = _ID);
	if (select count(GuildID)) = 0 then
		insert into Guild (DiscordID) values (_ID);
		insert into GuildPrefix values (last_insert_id(), _Prefix);
	else
		if (select count(ID) from GuildPrefix where ID = GuildID) = 0 then
			insert into GuildPrefix values (GuildID, _Prefix);
		else
			update GuildPrefix set Prefix = _Prefix where ID = GuildID;
		end if;
	end if;
end$$

create procedure SetLocale (
	in _ID bigint,
	in _Locale tinyint
) begin
	declare GuildID int;
	set GuildID = (select ID from Guild where DiscordID = _ID);
	if (select count(GuildID)) = 0 then
		insert into Guild (DiscordID) values (_ID);
		insert into GuildLocale values (last_insert_id(), _Locale);
	else
		if (select count(ID) from GuildPrefix where ID = GuildID) = 0 then
			insert into GuildLocale values (GuildID, _Locale);
		else
			update GuildLocale set Locale = _Locale where ID = GuildID;
		end if;
	end if;
end$$