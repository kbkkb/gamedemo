-- PlayerMove.lua

local PlayerMove = {}
PlayerMove.speed = 5

function PlayerMove:Awake()
    -- 获取玩家的Transform组件
    self.transform = self.gameObject.transform
end

function PlayerMove:Update()
    -- 获取水平输入（-1到1之间的值）
    local horizontal = UnityEngine.Input.GetAxis("Horizontal")
    -- 获取垂直输入
    local vertical = UnityEngine.Input.GetAxis("Vertical")

    -- 根据输入计算移动方向
    local move = UnityEngine.Vector3(horizontal, vertical, 0) * self.speed * UnityEngine.Time.deltaTime
    -- 移动玩家
    self.transform:Translate(move)
end

-- 返回 PlayerMove 表
return PlayerMove
