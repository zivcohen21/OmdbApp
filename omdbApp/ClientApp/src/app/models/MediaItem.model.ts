import { Season } from './Season.model';

export class MediaItem {
  imdbID: string;
  title: string;
  released: string;
  type: string;
  genre: string
  poster: string;
  plot: string;
  seasons: Season[];
}
