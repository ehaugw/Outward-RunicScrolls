include Makefile.helpers
modname = RunicScrolls
dependencies =

assemble:
	# common for all mods
	rm -f -r public
	@make dllsinto TARGET=$(modname) --no-print-directory
	
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
	
forceinstall:
	make assemble
	rm -r -f $(gamepath)/$(pluginpath)/$(modname)
	cp -u -r public/* $(gamepath)

play:
	(make install && cd .. && make play)
