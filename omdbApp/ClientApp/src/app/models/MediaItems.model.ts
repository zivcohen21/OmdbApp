import { Episode } from './Episode.model';

export class MediaItem {
  title: string;
  released: string;
  type: string;
  poster: string;
  plot: string;
  episodes: Episode[];
}
