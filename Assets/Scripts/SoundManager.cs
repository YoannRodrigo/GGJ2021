using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

[System.Serializable]
public class Sound {
	
	public string name;
	public bool isFadeIn,isFadeOut,finnishFade;
	public Coroutine fadeInCoroutine,fadeOutCoroutine,fadeOutVolumeCoroutine,fadeInVolumeCoroutine;
	public AudioClip clip;
	public float volume = 0.7f;
	[Range(0.5f,1.5f)]
	public float pitch = 1f;
	[Range(0f,1.5f)]
	public float randomVolume = 0.1f;
	[Range(0f,0.5f)]
	public float randomPitch = 0.1f;
	[Range(0f,0.5f)]

	float startVolume;
	
	AudioSource source;


	public void setSource(AudioSource _source){
		source = _source;
		source.clip = clip;
		startVolume = volume;
	}
	public void Play(){
		source.volume = volume * (1 + Random.Range(-randomVolume/2f, randomVolume/2f));
		source.pitch = pitch * (1 + Random.Range(-randomPitch/2f, randomPitch/2f));
		source.Play();	
	}
	public void PlayMusic(){
		source.volume = volume;
		source.pitch = pitch;
		source.Play();
	}
	public bool isPlaying(){
		return source.isPlaying;
	}
	public void makeItLoop(){
		source.loop = true;
	}
	public void Stop(){
		source.Stop();
	}
	public IEnumerator FadeOut (float FadeTime) {
        float startVolume = source.volume;
		isFadeOut = true;
        while (source.volume > 0) {
            source.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
		isFadeOut = false;
        source.Stop ();
        source.volume = startVolume;
	}
	public IEnumerator EndFadeOut(){
		isFadeOut = false;
        source.Stop ();
        source.volume = startVolume;
		yield return null;
	}
	public IEnumerator FadeIn (float FadeTime) {
		source.Play();
		source.volume = 0;
		isFadeIn = true;
        float endVolume = volume;
 
        while (source.volume < endVolume) {
            source.volume += endVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
		isFadeIn = false;
	}
	public IEnumerator EndFadeIn(){
		isFadeIn = false;
        source.Stop ();
        source.volume = 0;
		yield return null;
	}
	public IEnumerator FadeInVolume (float FadeTime) {
		source.volume = 0;
		isFadeIn = true;
        float endVolume = startVolume; 
        while (source.volume < endVolume) {
            source.volume += endVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
		isFadeIn = false;
	}
	public IEnumerator FadeOutVolume (float FadeTime) {
        float startVolume = source.volume;
		isFadeOut = true;
        while (source.volume > 0) {
            source.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;	
        }
		isFadeOut = false;
	}
	public IEnumerator EndFadeInVolume(){
		isFadeIn = false;
        source.volume = startVolume;
		yield return null;
	}
	
	public IEnumerator EndFadeOutVolume(){
		isFadeOut = false;
        source.volume = 0;
		yield return null;
	}
	


}
public class SoundManager : MonoBehaviour {
	
	public static SoundManager instance;
	[SerializeField]
	Sound[] sounds;
	[SerializeField]

	public Sound[] musiques;
	[SerializeField]

	public AudioMixerGroup MusicMixer;
	void Awake(){
		if(instance == null ){
		instance = this;
		}
		else if(instance != null && instance != this){
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);

		for(int i = 0; i < sounds.Length; i++){
			GameObject GO = new GameObject("Fx_" + i + "_" + sounds[i].name);
			GO.transform.SetParent(transform);
			AudioSource GOAudio = GO.AddComponent<AudioSource>();
			GOAudio.outputAudioMixerGroup = MusicMixer;
			sounds[i].setSource(GOAudio);
		}
		for(int i = 0; i < musiques.Length; i++){
			GameObject GO = new GameObject("Fx_" + i + "_" + musiques[i].name);
			GO.transform.SetParent(transform);
			AudioSource GOAudio = GO.AddComponent<AudioSource>();
			GOAudio.outputAudioMixerGroup = MusicMixer;
			musiques[i].setSource(GOAudio);
		}
	}

    public void PlaySound(string soundName){
		foreach(Sound _sound in sounds ){
			if(_sound.name == soundName){
				_sound.Play();
				return;
			}
		}
	}
	
	public void StopSound(string soundName){
		foreach(Sound _sound in sounds ){
			if(_sound.name == soundName){
				if(_sound.isPlaying()){
					_sound.Stop();
				}

			}
		}
	}
	public void PlaySoundLoop(string soundName){
		foreach(Sound _sound in sounds ){
			if(_sound.name == soundName){
				if(!_sound.isPlaying()){
					_sound.Play();
					return;
				}
			}
		}
	}
	public void PlayMusic(string musiqueName){
		foreach(Sound _sound in musiques ){
			if(_sound.name == musiqueName){
				if(!_sound.isPlaying()){
					_sound.makeItLoop();
					_sound.Play();
					return;
				}

			}
		}
	}
	public void FadeOutMusic(string musicName,float fadeTime){
		foreach(Sound _sound in musiques ){
			if(_sound.name == musicName){
				if(_sound.isPlaying()){
					if(_sound.isFadeIn){
						StopCoroutine(_sound.fadeInCoroutine);
						StartCoroutine(_sound.EndFadeIn());
					}
					_sound.fadeOutCoroutine = StartCoroutine(_sound.FadeOut(fadeTime));
				}
			}
		}
	}

	public void FadeInMusic(string musicName,float fadeTime, bool overPlay = false){
		foreach(Sound _sound in musiques ){
			if(_sound.name == musicName){
				if(overPlay){
					if(_sound.isFadeOut || _sound.isFadeIn){
						if(_sound.isFadeOut){
							StopCoroutine(_sound.fadeOutCoroutine);
							StartCoroutine(_sound.EndFadeOut());
						}
						if(_sound.isFadeIn){
							
							StopCoroutine(_sound.fadeInCoroutine);
							StartCoroutine(_sound.EndFadeIn());
						}

					}
				}
				if(!_sound.isPlaying()){
					_sound.fadeInCoroutine = StartCoroutine(_sound.FadeIn(fadeTime));
					_sound.makeItLoop();
				}
			}
		}
	}
	public void StopMusic(string musiqueName){
		foreach(Sound _sound in musiques ){
			if(_sound.name == musiqueName){
				if(_sound.isPlaying()){
					_sound.Stop();
				}

			}
		}
	}
	public void PlayRandomSound(string[] SoundsNames){
		string soundToPlay = SoundsNames[Random.Range(0,SoundsNames.Length-1)];
		PlaySound(soundToPlay);
	}
	public void PlayRandomMusic(string[] musicNames){
		string musicToPlay = musicNames[Random.Range(0,musicNames.Length-1)];
		PlayMusic(musicToPlay);
	}
	public void PlayMusicMuted(string musicName){
		foreach(Sound _sound in musiques ){
			if(_sound.name == musicName){
				if(!_sound.isPlaying()){
					_sound.volume = 0;
					_sound.makeItLoop();
					_sound.Play();
					return;
				}
			}
		}
	}
	public void FadeInMusicVolume(string musicName,float fadeTime, bool overPlay = false,float fadoutTime = .3f){
		foreach(Sound _sound in musiques ){
			if(_sound.name == musicName){
				if(overPlay){
					if(_sound.isFadeOut || _sound.isFadeIn){
						if(_sound.isFadeOut){
							StopCoroutine(_sound.fadeOutVolumeCoroutine);
							StartCoroutine(_sound.EndFadeOutVolume());
						}
						if(_sound.isFadeIn){
							StopCoroutine(_sound.fadeInVolumeCoroutine);
							StartCoroutine(_sound.EndFadeInVolume());
						}

					}
				}
				_sound.fadeInVolumeCoroutine = StartCoroutine(_sound.FadeInVolume(fadeTime));
				_sound.makeItLoop();
			}
		}
	}
		public void FadeOutMusicVolume(string musicName,float fadeTime){
		foreach(Sound _sound in musiques ){
			if(_sound.name == musicName){
				if(_sound.isPlaying()){
					if(_sound.isFadeIn){
						StopCoroutine(_sound.fadeInVolumeCoroutine);
						StartCoroutine(_sound.EndFadeInVolume());
					}
					_sound.fadeOutVolumeCoroutine = StartCoroutine(_sound.FadeOutVolume(fadeTime));
				}
			}
		}
	}
	public bool SoundIsPlaying(string soundName){
		foreach(Sound _sound in musiques ){
			if(_sound.name == soundName){
				if(_sound.isPlaying()){
					return true;
				}	
			}
		}
		foreach(Sound _sound in sounds ){
			if(_sound.name == soundName){
				if(_sound.isPlaying()){
					return true;
				}	
			}
		}
		return false;
	}

	public void FadeAllMusicsAndSounds(){
		foreach(Sound _sound in musiques ){
			if(_sound.isPlaying()){
				FadeOutMusic(_sound.name,.5f);
			}	
		}
		foreach(Sound s in sounds ){
			if(s.isPlaying()){
				StopSound(s.name);
			}	
		}
	}

}
