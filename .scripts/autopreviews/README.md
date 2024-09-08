# autopreviews

## Video id format

2 symbols championship (f1, f2, f3...)  
2 symbols year (22, 23...)  
2 symbols grandprix (01, 02...)  
2 symbols type (p1, qv, rc...)  
*e.g.* `f12201rc`

## Usage

1. Put backgrounds (1920 x 1080) into `sources/backgrounds` and name them in format `{championship}{year}{grandprix}.png`. Same background will be used for all videos in the grand prix.  
2. Put countries' flags (200 x 113) into `sources/flags/{championship}{year}` and name them in format `{grandprix}.png`.  
3. Put `{championship}_frame.png` (1870 x 1030), `{championship}_logo.png` (320 x ~100) and `grandprix_line.png` (1920 x ~120) into `sources/static`. `grandprix_line.png` will be used as a background for the text.  
4. Update `sources/text/titles.json` to localize the grand prix titles or use those that already exist.  
5. Put the desired font into `sources/text` as `font.ttf`.  
6. Generate previews using `generate.sh {video1_id} {video2_id} {...}` and locate them in the `output` directory.
