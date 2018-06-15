import { Component, OnInit } from '@angular/core';
import { SearchService } from '../services/search.service';
import { MediaItem } from '../models/MediaItems.model';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { animate, state, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
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

  public itemsList: MediaItem[];
  p: number = 1;
  ipp: number = 10;
  public start: boolean = true;

  isExpansionDetailRow = (i: number, row: Object) => row.hasOwnProperty('detailRow');
  expandedElement: any;

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
        console.info(data);
        this.itemsList = data;
        console.info(this.itemsList);
        this.start = false;
      }
      //error => {
          
      //  }
    );
  }

  expandItem(item) {

  }
}

