#pragma once

struct jvs_key_bind {
	int Test;
	int Start;
	int Service;
	int Up;
	int Left;
	int Down;
	int Right;
	int Button1;
	int Button2;
	int Button3;
	int Button4;
};

void InitializeJvs(jvs_key_bind keyBind);