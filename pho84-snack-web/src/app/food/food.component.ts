import { Component, OnInit } from "@angular/core";
import { Category } from "../models";
import { DataService } from "../services/data.service";

@Component({
  selector: "app-food",
  templateUrl: "./food.component.html",
  styleUrls: ["./food.component.css"]
})
export class FoodComponent implements OnInit {
  categories: Category[];

  constructor(private ds: DataService) {}

  ngOnInit() {
    this.ds.category.subscribe((categories: Category[]) => {
      this.categories = categories.filter(c => c.type === "food");
    });
  }
}
