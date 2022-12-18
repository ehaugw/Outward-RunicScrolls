modname = RunicScrolls
gamepath = /mnt/c/Program\ Files\ \(x86\)/Steam/steamapps/common/Outward/Outward_Defed
pluginpath = BepInEx/plugins
sideloaderpath = $(pluginpath)/$(modname)/SideLoader

assemble:
	# common for all mods
	rm -f -r public
	mkdir -p public/$(pluginpath)/$(modname)
	cp -u bin/$(modname).dll public/$(pluginpath)/$(modname)/
	
	# sideloader specific
	mkdir -p public/$(sideloaderpath)/Items
	mkdir -p public/$(sideloaderpath)/Texture2D
	mkdir -p public/$(sideloaderpath)/AssetBundles
	
	mkdir -p public/$(sideloaderpath)/Items/RunicScroll/Textures/mat_itm_bowRecurve
	cp -u resources/icons/runic_scroll.png                     public/$(sideloaderpath)/Items/RunicScroll/Textures/icon.png
	cp -u resources/textures/runic_scroll_norm.png             public/$(sideloaderpath)/Items/RunicScroll/Textures/mat_itm_bowRecurve/_NormTex.png
	cp -u resources/textures/runic_scroll_gen.png              public/$(sideloaderpath)/Items/RunicScroll/Textures/mat_itm_bowRecurve/_GenTex.png
	cp -u resources/textures/runic_scroll_main.png             public/$(sideloaderpath)/Items/RunicScroll/Textures/mat_itm_bowRecurve/_MainTex.png
	cp -u resources/textures/runic_scroll.xml                  public/$(sideloaderpath)/Items/RunicScroll/Textures/mat_itm_bowRecurve/properties.xml
	
	cp -u resources/textures/quiverDisplayRunicScroll.png      public/$(sideloaderpath)/Texture2D/
	cp -u resources/textures/tex_men_arrowQuiverIndicator.png  public/$(sideloaderpath)/Texture2D/
	
	cp -u resources/unity/RunicSCrolls/Assets/AssetBundles/scrollquiver     public/$(sideloaderpath)/AssetBundles/
	
publish:
	make assemble
	rm -f $(modname).rar
	rar a $(modname).rar -ep1 public/*

install:
	make assemble
	rm -r -f $(gamepath)/$(pluginpath)/$(modname)
	cp -u -r public/* $(gamepath)
clean:
	rm -f -r public
	rm -f $(modname).rar
	rm -f -r bin
info:
	echo Modname: $(modname)
