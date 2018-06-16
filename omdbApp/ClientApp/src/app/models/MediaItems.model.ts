import { Episode } from './Episode.model';

export class MediaItem {
  imdbID: string;
  title: string;
  released: string;
  type: string;
  poster: string;
  plot: string;
  episodes: Episode[];
}
