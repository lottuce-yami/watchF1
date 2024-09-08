#!/bin/bash

generate_preview() {
	champinoship=$1
	year=$2
	grandprix=$3
	type=$4
	base_id=$1$2$3
	full_id=$1$2$3$4

	background="./sources/backgrounds/${base_id}.png"
	frame="./sources/static/${champinoship}_frame.png"
	logo="./sources/static/${champinoship}_logo.png"
	flag="./sources/flags/${champinoship}${year}/${grandprix}.png"
	font="./sources/text/font.ttf"

	case $champinoship in
		f1 )
			color="#DE363D"
			;;
		f2 )
			color="#36B6DE"
			;;
		f3 )
			color="#363636"
			;;
	esac

	case $type in
		p1 )
			type_text="СВОБОДНЫЕ ЗАЕЗДЫ 1"
			font_size="128"
			;;
		p2 )
			type_text="СВОБОДНЫЕ ЗАЕЗДЫ 2"
			font_size="128"
			;;
		p3 )
			type_text="СВОБОДНЫЕ ЗАЕЗДЫ 3"
			font_size="128"
			;;
		sq )
			type_text="СПРИНТ-КВАЛИФИКАЦИЯ"
			font_size="128"
			;;
		sp )
			type_text="СПРИНТ"
			font_size="160"
			;;
		qv )
			type_text="КВАЛИФИКАЦИЯ"
			font_size="128"
			;;
		rc )
			type_text="ГОНКА"
			font_size="160"
			;;
	esac

	grandprix_text=$(jq -r --arg season "${champinoship}${year}" --arg grandprix "$grandprix" '.[$season] | .[$grandprix]' "./sources/text/titles.json")

	ffmpeg -i "${background}" -i "./sources/static/grandprix_line.png" -i "${frame}" -i "${logo}" -i "${flag}" -filter_complex "overlay=y=H-h-75,overlay=x=25:y=25,overlay=x=W-w-45:y=45,overlay=y=75,drawtext=fontfile='${font}':fontsize=${font_size}:fontcolor=white:shadowy=6:y=H-th-230:box=1:boxborderw=15:boxcolor=${color}:text='\  ${type_text}',drawtext=fontfile='${font}':fontsize=96:fontcolor=white:shadowy=6:x=60:y=H-th-85:text='${grandprix_text}'" -y "./output/${full_id}.png"
}

for video in "$@"
do
	generate_preview ${video::2} ${video:2:2} ${video:4:2} ${video:6:2}
done
