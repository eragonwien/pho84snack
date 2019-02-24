import { Component, OnInit, Input } from '@angular/core';
import { Feature } from 'src/app/models';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'app-home-featured',
  templateUrl: './home-featured.component.html',
  styleUrls: ['./home-featured.component.css']
})
export class HomeFeaturedComponent implements OnInit {

  features: Feature[];

  constructor(private ds: DataService) { }

  ngOnInit() {
    this.ds.features.subscribe((res: Feature[]) => {
      this.features = res;
    });
  }

  backgroundImg(feature: Feature)
  {
    return "url('/assets/images/compressed/" + feature.image + ".jpg')";
  }
}
