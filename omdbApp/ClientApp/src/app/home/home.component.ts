import { Component, OnInit } from '@angular/core';
import { SearchService } from '../services/search.service';
import { MediaItem } from '../models/MediaItem.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit{

  model: any = {};

  response: { mediaItems: MediaItem[], message: string };
  itemsList: MediaItem[] = [];
  p: number = 1;
  ipp: number = 10;
  start: boolean = true;
  expanedItem: number = -1;
  loading: boolean = false;

  constructor(private searchService: SearchService) { }
  
  ngOnInit() {
    
  }

  search() {
    console.info(this.model);
    this.itemsList = [];
    this.start = true;
    this.loading = true;
    this.expanedItem = -1;
    this.searchService.sendSearch(this.model)
      .subscribe(
      data => {
        this.response = data;
        console.info(data);
        this.itemsList = data.mediaItems;
        this.start = false;
        this.loading = false;
      }
    );
  }

  itemClicked(itemIndex)
  {
    if (this.expanedItem == itemIndex) {
      this.expanedItem = -1;
    }
    else {
      this.expanedItem = itemIndex;
    }
    
  }
}


