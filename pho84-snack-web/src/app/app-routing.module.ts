import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { ContactComponent } from "./contact/contact.component";
import { DrinksComponent } from "./drinks/drinks.component";
import { FoodComponent } from "./food/food.component";

const routes: Routes = [
  { path: "", pathMatch: "full", redirectTo: "/home" },
  { path: "home", component: HomeComponent, data: { animation: "home" } },
  { path: "kontakt", component: ContactComponent, data: { animation: "kontakt" } },
  { path: "essen", component: FoodComponent, data: { animation: "essen" } },
  { path: "trinken", component: DrinksComponent, data: { animation: "trinken" } }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true, scrollPositionRestoration: "top" })],
  exports: [RouterModule]
})
export class AppRoutingModule {}
