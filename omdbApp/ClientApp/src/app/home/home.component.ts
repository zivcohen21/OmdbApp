import { Component, OnInit } from '@angular/core';
import { SearchService } from '../services/search.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit{

  model: any = {};

  constructor(private searchService: SearchService) { }


  ngOnInit() {
    
  }

  search() {
    console.info(this.model);
    this.searchService.sendSearch(this.model)
      .subscribe(
      data => {
          
        },
      error => {

        });
  }
}
