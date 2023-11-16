import { Component } from '@angular/core';
import { GameService } from '../../services/gameService/game.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent {
  constructor(
    private gameService: GameService
  ) { }

  isLoading: boolean = true;
  text: string = '';

  ngOnInit(): void {
    setTimeout(() => {
      this.text = this.gameService.getDefaultText();
      this.isLoading = false;
    }, 3000)
  }
}
