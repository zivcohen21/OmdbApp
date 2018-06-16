import { Component, OnInit } from '@angular/core';
import { SearchService } from '../services/search.service';
import { MediaItem } from '../models/MediaItem.model';
import { DataSource } from '@angular/cdk/collections';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0', visibility: 'hidden' })),
      state('expanded', style({ height: '*', visibility: 'visible' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class HomeComponent implements OnInit{

  model: any = {};

  public response: { mediaItems: MediaItem[], message: string };
  public itemsList: MediaItem[] = [];
  p: number = 1;
  ipp: number = 10;
  public start: boolean = true;
  expanedItem: number = -1;

  constructor(private searchService: SearchService) { }
  
  ngOnInit() {
    
  }

  search() {
    console.info(this.model);
    this.itemsList = [];
    this.start = true;
    this.searchService.sendSearch(this.model)
      .subscribe(
      data => {
        this.response = data;
        console.info(data);
        this.itemsList = data.mediaItems;
        this.start = false;
      }
      //error => {
          
      //  }
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


